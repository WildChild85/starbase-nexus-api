using AutoMapper;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Models.Social.Like;

namespace starbase_nexus_api.Profiles.Social
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<Like, ViewLike>();
            CreateMap<CreateLike, Like>();
        }
    }
}
