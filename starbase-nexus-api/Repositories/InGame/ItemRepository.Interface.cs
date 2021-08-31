using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.InGame.Item;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.InGame
{
    public interface IItemRepository<EntityType> : IUuidBaseRepository<EntityType> where EntityType : Item
    {
        Task<PagedList<Item>> GetMultiple(ItemSearchParameters parameters);
    }
}
