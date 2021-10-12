using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.ShipShopSpot;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public interface IShipShopSpotRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : ShipShopSpot
    {
        Task<PagedList<ShipShopSpot>> GetMultiple(ShipShopSpotSearchParameters parameters);
    }
}
