using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Constructions
{
    public class ShipClassRepository : UuidBaseRepository<ShipClass>, IShipClassRepository<ShipClass>
    {
        public ShipClassRepository(MainDbContext context) : base(context)
        {

        }
        public override async Task<PagedList<ShipClass>> GetMultiple(SearchParameters parameters)
        {
            IQueryable<ShipClass> collection = _dbSet as IQueryable<ShipClass>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Name.Contains(parameters.SearchQuery)
                    ||
                    r.Description.Contains(parameters.SearchQuery)
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<ShipClass> pagedList = await PagedList<ShipClass>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
