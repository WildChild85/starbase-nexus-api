using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Entities
{
    public abstract class UuidBaseEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
