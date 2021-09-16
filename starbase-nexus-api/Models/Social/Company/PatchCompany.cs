using starbase_nexus_api.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Models.Social.Company
{
    public class PatchCompany
    {
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string AboutUs { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? LogoUri { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? DiscordUri { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? WebsiteUri { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? YoutubeUri { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? TwitchUri { get; set; }
    }
}
