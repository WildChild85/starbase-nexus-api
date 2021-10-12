using AutoMapper;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Models.Social.Rating;

namespace starbase_nexus_api.Profiles.Social
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<Rating, ViewRating>();
            CreateMap<CreateRating, Rating>();
            CreateMap<PatchRating, Rating>().ReverseMap();
        }
    }
}
