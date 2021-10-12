using starbase_nexus_api.Entities.Constructions;

namespace starbase_nexus_api.Repositories.Constructions
{
    public interface IShipRoleRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : ShipRole
    {
    }
}
