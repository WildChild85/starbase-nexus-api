using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public class ShipShopRepository : UuidBaseRepository<ShipShop>, IShipShopRepository<ShipShop>
    {
        public ShipShopRepository(MainDbContext context) : base(context)
        {

        }
        public override async Task<PagedList<ShipShop>> GetMultiple(SearchParameters parameters)
        {
            IQueryable<ShipShop> collection = _dbSet as IQueryable<ShipShop>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Name.Contains(parameters.SearchQuery)
                    ||
                    r.Description.Contains(parameters.SearchQuery)
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<ShipShop> pagedList = await PagedList<ShipShop>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
