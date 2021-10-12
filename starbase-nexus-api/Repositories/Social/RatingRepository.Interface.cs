using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Social.Rating;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Social
{
    public interface IRatingRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : Rating
    {
        Task<PagedList<Rating>> GetMultiple(RatingSearchParameters parameters);
    }
}
