using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Models.Identity.Role;
using AutoMapper;

namespace starbase_nexus_api.Profiles.Identity
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, ViewRole>();
            CreateMap<CreateRole, Role>();
            CreateMap<PatchRole, Role>().ReverseMap();
        }
    }
}
