using AutoMapper;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Models.Constructions.ShipMaterialCost;

namespace starbase_nexus_api.Profiles.Constructions
{
    public class ShipMaterialCostProfile : Profile
    {
        public ShipMaterialCostProfile()
        {
            CreateMap<ShipMaterialCost, ViewShipMaterialCost>();
            CreateMap<CreateShipMaterialCost, ShipMaterialCost>();
            CreateMap<PatchShipMaterialCost, ShipMaterialCost>().ReverseMap();
        }
    }
}
