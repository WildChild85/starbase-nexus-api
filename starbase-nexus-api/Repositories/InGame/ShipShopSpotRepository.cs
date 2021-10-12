using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.ShipShopSpot;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public class ShipShopSpotRepository : UuidBaseRepository<ShipShopSpot>, IShipShopSpotRepository<ShipShopSpot>
    {
        public ShipShopSpotRepository(MainDbContext context) : base(context)
        {

        }
        public virtual async Task<PagedList<ShipShopSpot>> GetMultiple(ShipShopSpotSearchParameters parameters)
        {
            IQueryable<ShipShopSpot> collection = _dbSet as IQueryable<ShipShopSpot>;

            if (parameters.ShipShopIds != null)
            {
                List<Guid> shipShopIds = TextService.GetGuidArray(parameters.ShipShopIds, ',').ToList();
                if (shipShopIds.Count > 0)
                {
                    collection = collection.Where(r => shipShopIds.Contains(r.ShipShopId));
                }
            }

            if (parameters.ShipIds != null)
            {
                List<Guid> shipIds = TextService.GetGuidArray(parameters.ShipIds, ',').ToList();
                if (shipIds.Count > 0)
                {
                    collection = collection.Where(r => r.ShipId != null && shipIds.Contains((Guid)r.ShipId));
                }
            }

            if (parameters.Position != null)
                collection = collection.Where(r => r.Position == parameters.Position);

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<ShipShopSpot> pagedList = await PagedList<ShipShopSpot>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
