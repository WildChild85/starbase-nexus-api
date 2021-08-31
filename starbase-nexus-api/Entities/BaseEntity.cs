using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities
{
    public abstract class BaseEntity
    {
        public uint? OldId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
