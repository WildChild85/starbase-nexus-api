using starbase_nexus_api.Constants;
using starbase_nexus_api.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.InGame
{
    public class ShipShop : UuidBaseEntity
    {
        [ForeignKey("ModeratorId")]
        [MaxLength(InputSizes.GUID_MAX_LENGTH)]
        public string? ModeratorId { get; set; }
        public virtual User? Moderator { get; set; }

        [Url]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? ImageUri { get; set; }

        [Required]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string Description { get; set; }

        [Required]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Layout { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public int? Left { get; set; }

        public int? Top { get; set; }
    }
}
