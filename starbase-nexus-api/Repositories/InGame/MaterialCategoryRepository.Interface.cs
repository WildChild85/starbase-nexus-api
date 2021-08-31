using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.Api;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public interface IMaterialCategoryRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : MaterialCategory
    {
        Task<PagedList<MaterialCategory>> GetMultiple(SearchParameters parameters);
    }
}
