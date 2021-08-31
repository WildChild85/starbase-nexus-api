using starbase_nexus_api.Entities.Identity;
using System;
using System.Threading.Tasks;

namespace starbase_nexus_api.Services.Authentication
{
    public interface IBaseJwtService
    {
        Task<string> GetAccessToken(User user);
        Guid GetUserIdFromAccessToken(string accessToken);
    }
}