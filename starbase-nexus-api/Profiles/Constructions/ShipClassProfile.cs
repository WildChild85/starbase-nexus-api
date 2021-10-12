using AutoMapper;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Models.Constructions.ShipClass;

namespace starbase_nexus_api.Profiles.Constructions
{
    public class ShipClassProfile : Profile
    {
        public ShipClassProfile()
        {
            CreateMap<ShipClass, ViewShipClass>();
            CreateMap<CreateShipClass, ShipClass>();
            CreateMap<PatchShipClass, ShipClass>().ReverseMap();
        }
    }
}
