namespace starbase_nexus_api.Models.Social.Company
{
    public class ViewCompany : UuidViewModel
    {
        public string Name { get; set; }

        public string AboutUs { get; set; }

        public string? LogoUri { get; set; }

        public string? DiscordUri { get; set; }

        public string? WebsiteUri { get; set; }

        public string? YoutubeUri { get; set; }

        public string? TwitchUri { get; set; }

        public string CreatorId { get; set; }
    }
}
