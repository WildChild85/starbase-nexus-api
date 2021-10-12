using starbase_nexus_api.Entities.InGame;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.Constructions
{
    public class ShipMaterialCost : UuidBaseEntity
    {
        [Required]
        [ForeignKey("ShipId")]
        public Guid ShipId { get; set; }
        [Required]
        public virtual Ship Ship { get; set; }

        [Required]
        [ForeignKey("MaterialId")]
        public Guid MaterialId { get; set; }
        [Required]
        public virtual Material Material { get; set; }

        [Required]
        public float Voxels { get; set; }
    }
}
