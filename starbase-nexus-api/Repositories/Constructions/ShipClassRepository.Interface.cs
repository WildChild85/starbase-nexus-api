using starbase_nexus_api.Entities.Constructions;

namespace starbase_nexus_api.Repositories.Constructions
{
    public interface IShipClassRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : ShipClass
    {
    }
}
