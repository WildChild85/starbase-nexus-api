using starbase_nexus_api.Models.Api;

namespace starbase_nexus_api.Models.Social.Rating
{
    public class RatingSearchParameters : SearchParameters
    {
        public string? ShipIds { get; set; }

        public string? UserIds { get; set; }
    }
}
