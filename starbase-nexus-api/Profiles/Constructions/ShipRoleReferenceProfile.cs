using AutoMapper;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Models.Constructions.ShipRoleReference;

namespace starbase_nexus_api.Profiles.Constructions
{
    public class ShipRoleReferenceProfile : Profile
    {
        public ShipRoleReferenceProfile()
        {
            CreateMap<ShipRoleReference, ViewShipRoleReference>();
        }
    }
}
