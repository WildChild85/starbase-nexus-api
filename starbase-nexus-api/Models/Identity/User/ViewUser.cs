using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace starbase_nexus_api.Models.Identity.User
{
    public class ViewUser : ExternalViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public DateTimeOffset? LastLogin { get; set; }

        public bool EmailConfirmed { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public bool LockoutEnabled { get; set; }

        public uint AccessFailedCount { get; set; }


        public string? AboutMe { get; set; }

        public string? AvatarUri { get; set; }

        public DateTime? Birthday { get; set; }

        public string? FacebookUserId { get; set; }

        public string? FanOf { get; set; }

        public string FirstName { get; set; }

        public char? Gender { get; set; }

        public string? GoogleUserId { get; set; }

        public string? LastName { get; set; }

        public uint? OldId { get; set; }

        public virtual List<string> Roles { get; set; } = new List<string>();
    }
}
