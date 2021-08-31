using starbase_nexus_api.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Entities.Identity
{
    public class User : IdentityUser
    {
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? AboutMe { get; set; }

        [Url]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? AvatarUri { get; set; }

        public string DiscordId { get; set; }

        public DateTimeOffset? LastLogin { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
