using starbase_nexus_api.Models.Authentication.Discord;
using System.Threading.Tasks;

namespace starbase_nexus_api.Services.Authentication
{
    public interface IDiscordService
    {
        Task<string> GetAccessToken(string code, string redirectUrl);
        Task<DiscordMeResponse> GetUserInfo(string accessToken);
    }
}