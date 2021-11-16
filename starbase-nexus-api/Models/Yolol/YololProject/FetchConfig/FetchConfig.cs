using starbase_nexus_api.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Yolol.YololProject.FetchConfig
{
    public class FetchConfig
    {
        [Required]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public Uri Docs { get; set; }

        [Required]
        public List<FetchConfigScript> Scripts { get; set; } = new List<FetchConfigScript>();
    }
}
