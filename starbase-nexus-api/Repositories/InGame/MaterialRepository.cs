using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.Material;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public class MaterialRepository : UuidBaseRepository<Material>, IMaterialRepository<Material>
    {
        public MaterialRepository(MainDbContext context) : base(context)
        {

        }
        public virtual async Task<PagedList<Material>> GetMultiple(MaterialSearchParameters parameters)
        {
            IQueryable<Material> collection = _dbSet as IQueryable<Material>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Name.Contains(parameters.SearchQuery)
                    ||
                    r.Description.Contains(parameters.SearchQuery)
                );
            }

            if (parameters.MaterialCategoryIds != null)
            {
                List<Guid> materialIds = TextService.GetGuidArray(parameters.MaterialCategoryIds, ',').ToList();
                if (materialIds.Count > 0)
                {
                    collection = collection.Where(r => materialIds.Contains(r.MaterialCategoryId));
                }
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<Material> pagedList = await PagedList<Material>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
