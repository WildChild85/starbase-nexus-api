using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Yolol.YololProject.FetchConfig
{
    public class FetchConfigScript
    {
        [Required]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public Uri Uri { get; set; }

        [MaxLength(20)]
        public string? Name { get; set; }
    }
}
