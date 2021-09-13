using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Yolol.YololScript
{
    public class CreateYololScript
    {
        [Required]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        public string Code { get; set; }

        [Required]
        public Guid? ProjectId { get; set; }
    }
}
