using starbase_nexus_api.Models.Yolol.YololProject.FetchConfig;
using starbase_nexus_api.Models.Yolol.YololProject.Fetched;
using System.Threading.Tasks;

namespace starbase_nexus_api.Services.Yolol
{
    public interface IFetchConfigService
    {
        Task<FetchConfigValidationResult> ValidateFetchConfig(string fetchConfigUri);
        Task<FetchedYololProject> LoadProjectByFetchConfig(FetchConfig fetchConfig);
    }
}