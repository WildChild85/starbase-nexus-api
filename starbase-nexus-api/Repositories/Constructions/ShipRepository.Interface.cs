using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.Ship;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Constructions
{
    public interface IShipRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : Ship
    {
        Task<PagedList<Ship>> GetMultiple(ShipSearchParameters parameters);
    }
}
