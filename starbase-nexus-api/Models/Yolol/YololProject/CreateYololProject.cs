using starbase_nexus_api.Constants;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Yolol.YololProject
{
    public class CreateYololProject
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
