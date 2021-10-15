using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.ShipRoleReference;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
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

            if (parameters.ShipIds != null)
            {
                List<Guid> shipIds = TextService.GetGuidArray(parameters.ShipIds, ',').ToList();
                if (shipIds.Count > 0)
                {
                    collection = collection.Where(r => shipIds.Contains((Guid)r.ShipId));
                }
            }

            if (parameters.ShipRoleIds != null)
            {
                List<Guid> shipRoleIds = TextService.GetGuidArray(parameters.ShipRoleIds, ',').ToList();
                if (shipRoleIds.Count > 0)
                {
                    collection = collection.Where(r => shipRoleIds.Contains((Guid)r.ShipRoleId));
                }
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<ShipRoleReference> pagedList = await PagedList<ShipRoleReference>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
