using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Social.Rating
{
    public class CreateRating
    {
        [Required]
        [Range(1, 5)]
        public uint Stars { get; set; }

        [Required]
        public Guid? ShipId { get; set; }

        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? Comment { get; set; }
    }
}
