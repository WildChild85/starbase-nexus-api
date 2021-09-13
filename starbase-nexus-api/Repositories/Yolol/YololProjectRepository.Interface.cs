using starbase_nexus_api.Entities.Yolol;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Yolol.YololProject;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Yolol
{
    public interface IYololProjectRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : YololProject
    {
        Task<PagedList<YololProject>> GetMultiple(YololProjectSearchParameters parameters);
    }
}
