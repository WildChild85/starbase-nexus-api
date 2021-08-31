using AutoMapper;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Models.InGame.MaterialCategory;

namespace starbase_nexus_api.Profiles.InGame
{
    public class MaterialCategoryProfile : Profile
    {
        public MaterialCategoryProfile()
        {
            CreateMap<MaterialCategory, ViewMaterialCategory>();
            CreateMap<CreateMaterialCategory, MaterialCategory>();
            CreateMap<PatchMaterialCategory, MaterialCategory>().ReverseMap();
        }
    }
}
