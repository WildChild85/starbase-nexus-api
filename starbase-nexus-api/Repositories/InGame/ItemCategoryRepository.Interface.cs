using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.ItemCategory;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public interface IItemCategoryRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : ItemCategory
    {
        Task<PagedList<ItemCategory>> GetMultiple(ItemCategorySearchParameters parameters);
    }
}
