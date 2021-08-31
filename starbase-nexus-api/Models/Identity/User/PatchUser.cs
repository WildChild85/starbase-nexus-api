using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Identity.User
{
    public class PatchUser
    {

        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? AboutMe { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        [Url]
        public string? AvatarUri { get; set; }
    }
}
