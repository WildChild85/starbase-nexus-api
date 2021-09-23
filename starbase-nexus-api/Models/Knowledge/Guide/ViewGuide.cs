namespace starbase_nexus_api.Models.Knowledge.Guide
{
    public class ViewGuide : UuidViewModel
    {
        public string Title { get; set; }

        public string? Bodytext { get; set; }

        public string? YoutubeVideoUri { get; set; }

        public string CreatorId { get; set; }
    }
}
