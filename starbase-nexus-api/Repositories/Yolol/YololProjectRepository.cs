using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Yolol;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Yolol.YololProject;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Yolol
{
    public class YololProjectRepository : UuidBaseRepository<YololProject>, IYololProjectRepository<YololProject>
    {
        public YololProjectRepository(MainDbContext context) : base(context)
        {

        }
        public virtual async Task<PagedList<YololProject>> GetMultiple(YololProjectSearchParameters parameters)
        {
            IQueryable<YololProject> collection = _dbSet as IQueryable<YololProject>;

            if (parameters.CreatorIds != null)
            {
                List<string> creatorIds = parameters.CreatorIds.Split(',').ToList();
                if (creatorIds.Count > 0)
                {
                    collection = collection.Where(r => creatorIds.Contains(r.CreatorId));
                }
            }

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Name.Contains(parameters.SearchQuery)
                    ||
                    r.Documentation.Contains(parameters.SearchQuery)
                    ||
                    r.Creator.UserName.Contains(parameters.SearchQuery)
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<YololProject> pagedList = await PagedList<YololProject>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
