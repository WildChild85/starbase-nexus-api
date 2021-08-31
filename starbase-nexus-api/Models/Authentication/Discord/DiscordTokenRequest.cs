using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Authentication.Discord
{
    public class DiscordTokenRequest
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string RedirectUrl { get; set; }
    }
}
