using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using starbase_nexus_api.Constants;
using starbase_nexus_api.Entities.Authentication;
using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Authentication;
using starbase_nexus_api.Models.Authentication.Discord;
using starbase_nexus_api.Repositories.Authentication;
using starbase_nexus_api.Repositories.Identity;
using starbase_nexus_api.Services.Authentication;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace starbase_nexus_api.Controllers.Authentication
{
    [Route("authentication/[controller]")]
    public class SignInController : DefaultControllerTemplate
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDiscordService _discordService;
        public SignInController(
            SignInManager<User> signInManager,
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IAccessTokenService accessTokenService,
            IDiscordService discordService
        )
        {
            _signInManager = signInManager;
            _refreshTokenRepository = refreshTokenRepository;
            _accessTokenService = accessTokenService;
            _userRepository = userRepository;
            _discordService = discordService;
        }


        /// <summary>
        /// Sign in and receive all necessary authentication tokens with discord login.
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [Route("discord")]
        public async Task<ActionResult<AuthenticationTokens>> SignInWithDiscord([FromBody] DiscordTokenRequest parameters)
        {
            string? discordToken = await _discordService.GetAccessToken(parameters.Code, parameters.RedirectUri);
            DiscordMeResponse? userInfo = await _discordService.GetUserInfo(discordToken);

            if (userInfo.id == null)
            {
                return BadRequest(new ErrorResponse(new List<string> { ErrorCodes.DISCORD_USER_ID_EMPTY }));
            }

            User? user = await _userRepository.GetOneOrDefaultByUserInfo(userInfo);

            if (user == null)
            {
                user = new User
                {
                    UserName = userInfo.username,
                    Email = $"{userInfo.username.ToLower()}@starbase-nexus.net",
                    AvatarUri = userInfo.avatar != null && userInfo.avatar.Trim().Length > 0 ? $"https://cdn.discordapp.com/avatars/{userInfo.id}/{userInfo.avatar}.png" : null,
                    DiscordId = userInfo.id
                };

                string password = TextService.GeneratePassword();
                IdentityResult resultCreate = await _userRepository.Create(user, password);
                if (!resultCreate.Succeeded)
                {
                    return BadRequest(resultCreate.Errors);
                }
            }

            user = await _userRepository.GetOneOrDefault(user.Id);
            user.LastLogin = DateTime.Now;
            await _userRepository.Update(user);

            string ip = HttpContext.Connection.RemoteIpAddress.ToString();

            RefreshToken refreshToken = await _refreshTokenRepository.GetNewRefreshToken(user, ip);

            return Ok(await GetTokenResponseObject(user, refreshToken));
        }

        /// <summary>
        /// Renew your authentication tokens.
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<AuthenticationTokens>> Refresh([FromBody] RefreshTokenRequest requestRefreshToken)
        {
            Guid userId;
            try
            {
                userId = _accessTokenService.GetUserIdFromAccessToken(requestRefreshToken.AccessToken);
            }
            catch
            {
                return Unauthorized();
            }

            User? user = await _userRepository.GetOneOrDefault(userId.ToString());

            if (user == null)
                return Unauthorized();

            if (user.LockoutEnabled)
                return Unauthorized();

            RefreshToken? refreshToken = await _refreshTokenRepository.GetRefreshToken(requestRefreshToken, user);

            if (refreshToken == null)
                return Unauthorized();

            if (refreshToken.ExpiresAt < DateTimeOffset.UtcNow)
            {
                string ip = HttpContext.Connection.RemoteIpAddress.ToString();
                refreshToken = await _refreshTokenRepository.GetNewRefreshToken(user, ip);
            }

            user.LastLogin = DateTimeOffset.UtcNow;
            await _userRepository.Update(user);

            return Ok(await GetTokenResponseObject(user, refreshToken));
        }

        private async Task<AuthenticationTokens> GetTokenResponseObject(User user, RefreshToken refreshToken)
        {
            string accessToken = await _accessTokenService.GetAccessToken(user);

            return new AuthenticationTokens
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
