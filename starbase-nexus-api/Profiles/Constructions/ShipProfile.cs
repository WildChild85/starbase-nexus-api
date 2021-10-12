using AutoMapper;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Models.Constructions.Ship;

namespace starbase_nexus_api.Profiles.Constructions
{
    public class ShipProfile : Profile
    {
        public ShipProfile()
        {
            CreateMap<Ship, ViewShip>();
            CreateMap<CreateShip, Ship>();
            CreateMap<PatchShip, Ship>().ReverseMap();
        }
    }
}
