using AutoMapper;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Models.Constructions.ShipRole;

namespace starbase_nexus_api.Profiles.Constructions
{
    public class ShipRoleProfile : Profile
    {
        public ShipRoleProfile()
        {
            CreateMap<ShipRole, ViewShipRole>();
            CreateMap<CreateShipRole, ShipRole>();
            CreateMap<PatchShipRole, ShipRole>().ReverseMap();
        }
    }
}
