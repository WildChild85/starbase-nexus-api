using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Social.Like;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Social
{
    public class LikeRepository : UuidBaseRepository<Like>, ILikeRepository<Like>
    {
        public LikeRepository(MainDbContext context) : base(context)
        {

        }
        public virtual async Task<PagedList<Like>> GetMultiple(LikeSearchParameters parameters)
        {
            IQueryable<Like> collection = _dbSet as IQueryable<Like>;

            if (parameters.UserIds != null)
            {
                List<string> userIds = parameters.UserIds.Split(',').ToList();
                if (userIds.Count > 0)
                {
                    collection = collection.Where(r => userIds.Contains(r.UserId));
                }
            }

            if (parameters.YololProjectIds != null)
            {
                List<Guid> yololProjectIds = TextService.GetGuidArray(parameters.YololProjectIds, ',').ToList();
                if (yololProjectIds.Count > 0)
                {
                    collection = collection.Where(r => r.YololProjectId != null && yololProjectIds.Contains((Guid)r.YololProjectId));
                }
            }

            if (parameters.GuideIds != null)
            {
                List<Guid> guideIds = TextService.GetGuidArray(parameters.GuideIds, ',').ToList();
                if (guideIds.Count > 0)
                {
                    collection = collection.Where(r => r.GuideId != null && guideIds.Contains((Guid)r.GuideId));
                }
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<Like> pagedList = await PagedList<Like>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
