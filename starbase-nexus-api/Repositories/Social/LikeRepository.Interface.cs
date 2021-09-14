using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Social.Like;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Social
{
    public interface ILikeRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : Like
    {
        Task<PagedList<Like>> GetMultiple(LikeSearchParameters parameters);
    }
}
