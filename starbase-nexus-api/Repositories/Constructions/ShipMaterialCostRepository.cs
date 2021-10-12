using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.ShipMaterialCost;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Constructions
{
    public class ShipMaterialCostRepository : UuidBaseRepository<ShipMaterialCost>, IShipMaterialCostRepository<ShipMaterialCost>
    {
        public ShipMaterialCostRepository(MainDbContext context) : base(context)
        {

        }

        public virtual async Task<PagedList<ShipMaterialCost>> GetMultiple(ShipMaterialCostSearchParameters parameters)
        {
            IQueryable<ShipMaterialCost> collection = _dbSet as IQueryable<ShipMaterialCost>;

            if (parameters.MaterialIds != null)
            {
                List<Guid> materialIds = TextService.GetGuidArray(parameters.MaterialIds, ',').ToList();
                if (materialIds.Count > 0)
                {
                    collection = collection.Where(r => materialIds.Contains(r.MaterialId));
                }
            }

            if (parameters.ShipIds != null)
            {
                List<Guid> shipIds = TextService.GetGuidArray(parameters.ShipIds, ',').ToList();
                if (shipIds.Count > 0)
                {
                    collection = collection.Where(r => shipIds.Contains(r.ShipId));
                }
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<ShipMaterialCost> pagedList = await PagedList<ShipMaterialCost>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
