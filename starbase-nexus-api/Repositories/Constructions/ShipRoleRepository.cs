using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Constructions
{
    public class ShipRoleRepository : UuidBaseRepository<ShipRole>, IShipRoleRepository<ShipRole>
    {
        public ShipRoleRepository(MainDbContext context) : base(context)
        {

        }
        public override async Task<PagedList<ShipRole>> GetMultiple(SearchParameters parameters)
        {
            IQueryable<ShipRole> collection = _dbSet as IQueryable<ShipRole>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Name.Contains(parameters.SearchQuery)
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<ShipRole> pagedList = await PagedList<ShipRole>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
