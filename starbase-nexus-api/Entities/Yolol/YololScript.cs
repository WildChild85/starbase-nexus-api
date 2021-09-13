using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.Yolol
{
    public class YololScript : UuidBaseEntity
    {
        [Required]
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string Code { get; set; }

        [Required]
        [ForeignKey("ProjectId")]
        public Guid ProjectId { get; set; }
        [Required]
        public virtual YololProject Project { get; set; }
    }
}
