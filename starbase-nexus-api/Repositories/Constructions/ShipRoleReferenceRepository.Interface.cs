using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.ShipRoleReference;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Constructions
{
    public interface IShipRoleReferenceRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : ShipRoleReference
    {
        Task<PagedList<ShipRoleReference>> GetMultiple(ShipRoleReferenceSearchParameters parameters);
    }
}
