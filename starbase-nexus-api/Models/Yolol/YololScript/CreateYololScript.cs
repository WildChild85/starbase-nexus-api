using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Yolol.YololScript
{
    public class CreateYololScript
    {
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.SCRIPT_NAME_MAX_LENGTH)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        public string Code { get; set; }

        [Required]
        public Guid? ProjectId { get; set; }
    }
}
