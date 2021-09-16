using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Social
{
    public class CompanyRepository : UuidBaseRepository<Company>, ICompanyRepository<Company>
    {
        public CompanyRepository(MainDbContext context) : base(context)
        {

        }
        public override async Task<PagedList<Company>> GetMultiple(SearchParameters parameters)
        {
            IQueryable<Company> collection = _dbSet as IQueryable<Company>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Name.Contains(parameters.SearchQuery)
                    ||
                    r.Creator.UserName.Contains(parameters.SearchQuery)
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<Company> pagedList = await PagedList<Company>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
