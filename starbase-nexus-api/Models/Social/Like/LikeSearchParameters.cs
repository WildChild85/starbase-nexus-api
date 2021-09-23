using starbase_nexus_api.Models.Api;

namespace starbase_nexus_api.Models.Social.Like
{
    public class LikeSearchParameters : SearchParameters
    {
        public string? GuideIds { get; set; }

        public string? YololProjectIds { get; set; }

        public string? UserIds { get; set; }
    }
}
