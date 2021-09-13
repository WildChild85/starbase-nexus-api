namespace starbase_nexus_api.Models.Yolol.YololProject
{
    public class ViewYololProject : UuidViewModel
    {
        public string Name { get; set; }

        public string Documentation { get; set; }

        public string CreatorId { get; set; }

        public string? PreviewImageUri { get; set; }

        public string? YoutubeVideoUri { get; set; }
    }
}
