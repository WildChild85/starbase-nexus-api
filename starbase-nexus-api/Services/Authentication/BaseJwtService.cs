using starbase_nexus_api.Constants.Identity;
using starbase_nexus_api.Entities.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace starbase_nexus_api.Services.Authentication
{
    // Use this instead: https://piotrgankiewicz.com/2017/07/24/jwt-rsa-hmac-asp-net-core/
    public abstract class BaseJwtService : IBaseJwtService
    {
        protected string _issuer;
        protected string _audience;
        protected string _key;
        protected int _bearerTTL;

        /// <summary>
        /// Generate a actual json web token.
        /// </summary>
        public async Task<string> GetAccessToken(User user)
        {

            Claim[] claims = new[]
            {
                    new Claim(JwtClaims.ID, user.Id),
                    new Claim(JwtClaims.USERNAME, user.UserName)
                };

            byte[] keyBytes = Encoding.UTF8.GetBytes(_key);
            SymmetricSecurityKey symmetricSecKey = new SymmetricSecurityKey(keyBytes);
            SigningCredentials credentials = new SigningCredentials(symmetricSecKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(_issuer, _audience, claims, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(_bearerTTL), credentials);

            await EnrichPayload(token, user);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        /// <summary>
        /// This method allows to enrich the jwt with additional data. The deriving class can extend this behavior.
        /// </summary>
        protected virtual async Task EnrichPayload(JwtSecurityToken token, User user)
        {
            token.Payload.Add(JwtClaims.IAT, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        /// <summary>
        /// The purpose of this method is to validate a jwt access token and to return the contained user id.
        /// </summary>
        public Guid GetUserIdFromAccessToken(string accessToken)
        {
            TokenValidationParameters tokenValidationParamters = new TokenValidationParameters
            {
                ValidateAudience = true, // You might need to validate this one depending on your case
                ValidateIssuer = true,
                ValidateLifetime = false, // Do not validate lifetime here
                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_key)
                    ),
                ValidAudience = _audience,
                ValidIssuer = _issuer
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(accessToken, tokenValidationParamters, out securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token!");
            }

            string? userId = principal.FindFirst(JwtClaims.ID)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new SecurityTokenException($"Missing claim: {JwtClaims.ID}!");
            }

            return Guid.Parse(userId);
        }
    }
}
