using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.Constructions
{
    public class ShipRoleReference : UuidBaseEntity
    {
        [Required]
        [ForeignKey("ShipId")]
        public Guid ShipId { get; set; }
        [Required]
        public virtual Ship Ship { get; set; }

        [Required]
        [ForeignKey("ShipRoleId")]
        public Guid ShipRoleId { get; set; }
        public virtual ShipRole ShipRole { get; set; }
    }
}
