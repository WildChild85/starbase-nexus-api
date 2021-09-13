using System;

namespace starbase_nexus_api.Models.Social
{
    public class PublicUser : ExternalViewModel
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? LastLogin { get; set; }

        public bool LockoutEnabled { get; set; }


        public string? AboutMe { get; set; }

        public string? AvatarUri { get; set; }

        public string UserName { get; set; }
    }
}
