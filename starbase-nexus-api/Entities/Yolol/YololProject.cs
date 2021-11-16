using starbase_nexus_api.Constants;
using starbase_nexus_api.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.Yolol
{
    public class YololProject : UuidBaseEntity
    {
        [Required]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? Documentation { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? FetchConfigUri { get; set; }

        [Required]
        [ForeignKey("CreatorId")]
        [MaxLength(InputSizes.GUID_MAX_LENGTH)]
        public string CreatorId { get; set; }
        [Required]
        public virtual User Creator { get; set; }

        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        [Url]
        public string? PreviewImageUri { get; set; }

        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        [Url]
        public string? YoutubeVideoUri { get; set; }
    }
}
