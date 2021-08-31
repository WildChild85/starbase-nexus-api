using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.Item;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public class ItemRepository : UuidBaseRepository<Item>, IItemRepository<Item>
    {
        public ItemRepository(MainDbContext context) : base(context)
        {

        }
        public virtual async Task<PagedList<Item>> GetMultiple(ItemSearchParameters parameters)
        {
            IQueryable<Item> collection = _dbSet as IQueryable<Item>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    r.Name.Contains(parameters.SearchQuery, System.StringComparison.OrdinalIgnoreCase)
                    ||
                    r.Description.Contains(parameters.SearchQuery, System.StringComparison.OrdinalIgnoreCase)
                );
            }

            if (parameters.ItemCategoryIds != null)
            {
                List<Guid> offerIds = TextService.GetGuidArray(parameters.ItemCategoryIds, ',').ToList();
                if (offerIds.Count > 0)
                {
                    collection = collection.Where(r => offerIds.Contains(r.ItemCategoryId));
                }
            }

            if (parameters.PrimaryMaterialIds != null)
            {
                List<Guid> offerIds = TextService.GetGuidArray(parameters.PrimaryMaterialIds, ',').ToList();
                if (offerIds.Count > 0)
                {
                    collection = collection.Where(r => r.PrimaryMaterialId != null && offerIds.Contains((Guid)r.PrimaryMaterialId));
                }
            }

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<Item> pagedList = await PagedList<Item>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
