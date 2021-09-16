using starbase_nexus_api.Constants;
using starbase_nexus_api.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.Social
{
    public class Company : UuidBaseEntity
    {
        [Required]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string AboutUs { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? LogoUri { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? DiscordUri { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? WebsiteUri { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? YoutubeUri { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? TwitchUri { get; set; }

        [Required]
        [MaxLength(InputSizes.GUID_MAX_LENGTH)]
        [ForeignKey("CreatorId")]
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
    }
}
