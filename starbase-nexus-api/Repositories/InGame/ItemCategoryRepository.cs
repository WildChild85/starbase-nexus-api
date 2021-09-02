using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.ItemCategory;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public class ItemCategoryRepository : UuidBaseRepository<ItemCategory>, IItemCategoryRepository<ItemCategory>
    {
        public ItemCategoryRepository(MainDbContext context) : base(context)
        {

        }
        public virtual async Task<PagedList<ItemCategory>> GetMultiple(ItemCategorySearchParameters parameters)
        {
            IQueryable<ItemCategory> collection = _dbSet as IQueryable<ItemCategory>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Name.Contains(parameters.SearchQuery)
                    ||
                    r.Description.Contains(parameters.SearchQuery)
                );
            }

            if (parameters.ParentIds != null)
            {
                List<Guid> offerIds = TextService.GetGuidArray(parameters.ParentIds, ',').ToList();
                if (offerIds.Count > 0)
                {
                    collection = collection.Where(r => r.ParentId != null && offerIds.Contains((Guid)r.ParentId));
                }
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<ItemCategory> pagedList = await PagedList<ItemCategory>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
