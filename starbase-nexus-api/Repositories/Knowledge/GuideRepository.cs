using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Knowledge;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Knowledge
{
    public class GuideRepository : UuidBaseRepository<Guide>, IGuideRepository<Guide>
    {
        public GuideRepository(MainDbContext context) : base(context)
        {

        }
        public override async Task<PagedList<Guide>> GetMultiple(SearchParameters parameters)
        {
            IQueryable<Guide> collection = _dbSet as IQueryable<Guide>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Title.Contains(parameters.SearchQuery)
                    ||
                    r.Creator.UserName.Contains(parameters.SearchQuery)
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<Guide> pagedList = await PagedList<Guide>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
