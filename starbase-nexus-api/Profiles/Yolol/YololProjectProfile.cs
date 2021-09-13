using AutoMapper;
using starbase_nexus_api.Entities.Yolol;
using starbase_nexus_api.Models.Yolol.YololProject;

namespace starbase_nexus_api.Profiles.Yolol
{
    public class YololProjectProfile : Profile
    {
        public YololProjectProfile()
        {
            CreateMap<YololProject, ViewYololProject>();
            CreateMap<CreateYololProject, YololProject>();
            CreateMap<PatchYololProject, YololProject>().ReverseMap();
        }
    }
}
