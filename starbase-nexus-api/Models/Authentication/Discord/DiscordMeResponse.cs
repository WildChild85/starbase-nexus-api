namespace starbase_nexus_api.Models.Authentication.Discord
{
    public class DiscordMeResponse
    {
        public string username { get; set; }
        public string discriminator { get; set; }
        public bool mfa_enabled { get; set; }
        public string id { get; set; }
        public string? avatar { get; set; }
    }
}
