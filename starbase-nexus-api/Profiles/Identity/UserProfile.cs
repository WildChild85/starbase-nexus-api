using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Models.Identity.User;
using starbase_nexus_api.Models.Social;
using AutoMapper;

namespace starbase_nexus_api.Profiles.Identity
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ViewUser>();
            CreateMap<PatchUser, User>().ReverseMap();
            CreateMap<User, PublicUser>();
        }
    }
}
