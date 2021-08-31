using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.Material;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public interface IMaterialRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : Material
    {
        Task<PagedList<Material>> GetMultiple(MaterialSearchParameters parameters);
    }
}
