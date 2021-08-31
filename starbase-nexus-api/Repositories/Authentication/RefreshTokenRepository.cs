using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Authentication;
using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Models.Authentication;
using starbase_nexus_api.StaticServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Authentication
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly MainDbContext _dbContext;
        private readonly DbSet<RefreshToken> _refreshTokens;
        private readonly IOptions<JwtTokenOptions> _options;


        public RefreshTokenRepository(MainDbContext context, IOptions<JwtTokenOptions> options)
        {
            _dbContext = context;
            _options = options;
            _refreshTokens = context.Authentication_RefreshTokens;
        }


        public async Task<RefreshToken> GetNewRefreshToken(User user, string ip)
        {
            await DeleteExpiredRefreshTokens(user, false);

            RefreshToken refreshToken = GenerateRefreshToken(user, ip);
            await _refreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            return refreshToken;
        }


        public async Task<RefreshToken?> GetRefreshToken(RefreshTokenRequest requestRefreshToken, User user)
        {
            IQueryable<RefreshToken> collection = _refreshTokens as IQueryable<RefreshToken>;
            RefreshToken? refreshToken = await collection.FirstOrDefaultAsync(rt => rt.Token == requestRefreshToken.RefreshToken && rt.UserId == user.Id);

            return refreshToken;
        }


        public async Task DeleteRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokens.Remove(refreshToken);
            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteExpiredRefreshTokens(User user, bool saveChanges = true)
        {
            IQueryable<RefreshToken> collection = _refreshTokens as IQueryable<RefreshToken>;
            List<RefreshToken> expiredRefreshTokens = await collection.Where(rt => rt.UserId == user.Id && rt.ExpiresAt <= DateTimeOffset.UtcNow.Date).ToListAsync();
            if (expiredRefreshTokens.Count > 0)
            {
                _refreshTokens.RemoveRange(expiredRefreshTokens);
                if (saveChanges)
                    await _dbContext.SaveChangesAsync();
            }
        }

        private RefreshToken GenerateRefreshToken(User user, string ip)
        {
            string tokenString = TextService.GenerateToken();

            return new RefreshToken
            {
                Token = tokenString,
                UserId = user.Id,
                ExpiresAt = DateTimeOffset.UtcNow.AddDays(_options.Value.RefreshTTL),
                IpAddress = ip
            };
        }
    }
}
