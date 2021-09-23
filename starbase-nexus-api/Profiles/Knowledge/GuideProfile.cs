using AutoMapper;
using starbase_nexus_api.Entities.Knowledge;
using starbase_nexus_api.Models.Knowledge.Guide;

namespace starbase_nexus_api.Profiles.Knowledge
{
    public class GuideProfile : Profile
    {
        public GuideProfile()
        {
            CreateMap<Guide, ViewGuide>();
            CreateMap<CreateGuide, Guide>();
            CreateMap<PatchGuide, Guide>();
        }
    }
}
