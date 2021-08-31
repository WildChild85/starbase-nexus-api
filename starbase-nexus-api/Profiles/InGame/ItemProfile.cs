using AutoMapper;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.InGame.Item;

namespace starbase_nexus_api.Profiles.InGame
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ViewItem>();
            CreateMap<CreateItem, Item>();
            CreateMap<PatchItem, Item>().ReverseMap();
        }
    }
}
