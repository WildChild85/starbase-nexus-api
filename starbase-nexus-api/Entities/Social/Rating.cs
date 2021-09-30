using starbase_nexus_api.Constants;
using starbase_nexus_api.Entities.Contructions;
using starbase_nexus_api.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.Social
{
    public class Rating : UuidBaseEntity
    {
        [Required]
        [Range(1, 5)]
        public uint Stars { get; set; }

        [Required]
        [ForeignKey("ShipId")]
        public Guid ShipId { get; set; }
        [Required]
        public Ship Ship { get; set; }

        [Required]
        [MaxLength(InputSizes.GUID_MAX_LENGTH)]
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        [Required]
        public virtual User User { get; set; }

        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? Comment { get; set; }
    }
}
