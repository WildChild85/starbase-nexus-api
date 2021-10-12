using starbase_nexus_api.Entities.InGame;

namespace starbase_nexus_api.Repositories.InGame
{
    public interface IShipShopRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : ShipShop
    {
    }
}
