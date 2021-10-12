using AutoMapper;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.InGame.ShipShop;

namespace starbase_nexus_api.Profiles.InGame
{
    public class ShipShopProfile : Profile
    {
        public ShipShopProfile()
        {
            CreateMap<ShipShop, ViewShipShop>();
            CreateMap<CreateShipShop, ShipShop>();
            CreateMap<PatchShipShop, ShipShop>().ReverseMap();
        }
    }
}
