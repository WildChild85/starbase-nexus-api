using starbase_nexus_api.Constants;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Yolol.YololScript
{
    public class PatchYololScript
    {
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.SCRIPT_NAME_MAX_LENGTH)]
        public string? Name { get; set; }

        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        public string Code { get; set; }
    }
}
