using Microsoft.AspNetCore.Identity;
using System;

namespace starbase_nexus_api.Entities.Identity
{
    public class Role : IdentityRole
    {
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
