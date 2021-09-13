using starbase_nexus_api.Entities.Yolol;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Yolol.YololScript;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Yolol
{
    public interface IYololScriptRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : YololScript
    {
        Task<PagedList<YololScript>> GetMultiple(YololScriptSearchParameters parameters);
    }
}
