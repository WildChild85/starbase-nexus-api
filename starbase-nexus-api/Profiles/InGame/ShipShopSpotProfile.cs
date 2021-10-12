using AutoMapper;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.InGame.ShipShopSpot;

namespace starbase_nexus_api.Profiles.InGame
{
    public class ShipShopSpotProfile : Profile
    {
        public ShipShopSpotProfile()
        {
            CreateMap<ShipShopSpot, ViewShipShopSpot>();
            CreateMap<CreateShipShopSpot, ShipShopSpot>();
            CreateMap<PatchShipShopSpot, ShipShopSpot>().ReverseMap();
        }
    }
}
