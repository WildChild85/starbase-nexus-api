using AutoMapper;
using starbase_nexus_api.Entities.Yolol;
using starbase_nexus_api.Models.Yolol.YololScript;

namespace starbase_nexus_api.Profiles.Yolol
{
    public class YololScriptProfile : Profile
    {
        public YololScriptProfile()
        {
            CreateMap<YololScript, ViewYololScript>();
            CreateMap<CreateYololScript, YololScript>();
            CreateMap<PatchYololScript, YololScript>().ReverseMap();
        }
    }
}
