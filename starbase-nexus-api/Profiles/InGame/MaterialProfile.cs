using AutoMapper;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.InGame.Material;

namespace starbase_nexus_api.Profiles.InGame
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<Material, ViewMaterial>();
            CreateMap<CreateMaterial, Material>();
            CreateMap<PatchMaterial, Material>().ReverseMap();
        }
    }
}
