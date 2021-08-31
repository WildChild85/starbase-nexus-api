using starbase_nexus_api.Entities.Authentication;
using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Models.Authentication;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Authentication
{
    public interface IRefreshTokenRepository
    {
        Task DeleteExpiredRefreshTokens(User user, bool saveChanges = true);
        Task DeleteRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> GetNewRefreshToken(User user, string ip);
        Task<RefreshToken> GetRefreshToken(RefreshTokenRequest requestRefreshToken, User user);
    }
}