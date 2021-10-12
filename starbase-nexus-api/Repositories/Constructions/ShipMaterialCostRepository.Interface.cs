using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.ShipMaterialCost;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Constructions
{
    public interface IShipMaterialCostRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : ShipMaterialCost
    {
        Task<PagedList<ShipMaterialCost>> GetMultiple(ShipMaterialCostSearchParameters parameters);
    }
}
