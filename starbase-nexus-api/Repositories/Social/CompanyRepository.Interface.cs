using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Social.Company;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Social
{
    public interface ICompanyRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : Company
    {
        Task<PagedList<Company>> GetMultiple(SearchParameters parameters);
    }
}
