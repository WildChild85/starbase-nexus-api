using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Social.Rating;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Social
{
    public class RatingRepository : UuidBaseRepository<Rating>, IRatingRepository<Rating>
    {
        public RatingRepository(MainDbContext context) : base(context)
        {

        }
        public virtual async Task<PagedList<Rating>> GetMultiple(RatingSearchParameters parameters)
        {
            IQueryable<Rating> collection = _dbSet as IQueryable<Rating>;

            if (parameters.UserIds != null)
            {
                List<string> userIds = parameters.UserIds.Split(',').ToList();
                if (userIds.Count > 0)
                {
                    collection = collection.Where(r => userIds.Contains(r.UserId));
                }
            }

            if (parameters.ShipIds != null)
            {
                List<Guid> shipIds = TextService.GetGuidArray(parameters.ShipIds, ',').ToList();
                if (shipIds.Count > 0)
                {
                    collection = collection.Where(r => shipIds.Contains((Guid)r.ShipId));
                }
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<Rating> pagedList = await PagedList<Rating>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
