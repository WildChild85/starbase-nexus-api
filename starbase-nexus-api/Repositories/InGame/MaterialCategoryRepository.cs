using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public class MaterialCategoryRepository : UuidBaseRepository<MaterialCategory>, IMaterialCategoryRepository<MaterialCategory>
    {
        public MaterialCategoryRepository(MainDbContext context) : base(context)
        {

        }
        public virtual async Task<PagedList<MaterialCategory>> GetMultiple(SearchParameters parameters)
        {
            IQueryable<MaterialCategory> collection = _dbSet as IQueryable<MaterialCategory>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Name.Contains(parameters.SearchQuery)
                    ||
                    r.Description.Contains(parameters.SearchQuery)
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<MaterialCategory> pagedList = await PagedList<MaterialCategory>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
