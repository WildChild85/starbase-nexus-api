using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace starbase_nexus_api.Services.Authentication
{
    public class AccessTokenService : BaseJwtService, IAccessTokenService
    {
        private readonly UserManager<User> _userManager;

        public AccessTokenService(IOptions<JwtTokenOptions> jwtTokenOptions, UserManager<User> userManager)
        {
            _userManager = userManager;
            _issuer = jwtTokenOptions.Value.Issuer;
            _audience = jwtTokenOptions.Value.Audience;
            _key = jwtTokenOptions.Value.Key;
            _bearerTTL = jwtTokenOptions.Value.BearerTTL;
        }

        protected override async Task EnrichPayload(JwtSecurityToken token, User user)
        {
            token.Payload.Add("avatarUri", user.AvatarUri);
            token.Payload.Add("roles", await _userManager.GetRolesAsync(user));
        }
    }
}
