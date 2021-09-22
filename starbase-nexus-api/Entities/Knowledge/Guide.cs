using starbase_nexus_api.Constants;
using starbase_nexus_api.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Entities.Knowledge
{
    public class Guide : UuidBaseEntity
    {
        [Required]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Title { get; set; }

        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? Bodytext { get; set; }

        [Url]
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? YoutubeVideoUri { get; set; }

        [Required]
        [ForeignKey("CreatorId")]
        [MaxLength(InputSizes.GUID_MAX_LENGTH)]
        public string CreatorId { get; set; }
        [Required]
        public virtual User Creator { get; set; }
    }
}
