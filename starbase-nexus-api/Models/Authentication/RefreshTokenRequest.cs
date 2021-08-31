using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Authentication
{
    public class RefreshTokenRequest
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
