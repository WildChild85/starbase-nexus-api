using AutoMapper;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.InGame.ItemCategory;

namespace starbase_nexus_api.Profiles.InGame
{
    public class ItemCategoryProfile : Profile
    {
        public ItemCategoryProfile()
        {
            CreateMap<ItemCategory, ViewItemCategory>();
            CreateMap<CreateItemCategory, ItemCategory>();
            CreateMap<PatchItemCategory, ItemCategory>().ReverseMap();
        }
    }
}
