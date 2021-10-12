using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.ShipRoleReference;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Constructions
{
    public class ShipRoleReferenceRepository : UuidBaseRepository<ShipRoleReference>, IShipRoleReferenceRepository<ShipRoleReference>
    {
        public ShipRoleReferenceRepository(MainDbContext context) : base(context)
        {

        }
        public async Task<PagedList<ShipRoleReference>> GetMultiple(ShipRoleReferenceSearchParameters parameters)
        {
            IQueryable<ShipRoleReference> collection = _dbSet as IQueryable<ShipRoleReference>;

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<ShipRoleReference> pagedList = await PagedList<ShipRoleReference>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
