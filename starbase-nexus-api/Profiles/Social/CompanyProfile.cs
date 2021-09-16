using AutoMapper;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Models.Social.Company;

namespace starbase_nexus_api.Profiles.Social
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, ViewCompany>();
            CreateMap<CreateCompany, Company>();
            CreateMap<PatchCompany, Company>().ReverseMap();
        }
    }
}
