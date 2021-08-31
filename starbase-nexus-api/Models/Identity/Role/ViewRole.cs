using System;

namespace starbase_nexus_api.Models.Identity.Role
{
    public class ViewRole : ExternalViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
