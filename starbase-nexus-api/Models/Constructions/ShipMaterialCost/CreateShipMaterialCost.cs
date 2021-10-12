using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Constructions.ShipMaterialCost
{
    public class CreateShipMaterialCost
    {
        [Required]
        public Guid? ShipId { get; set; }

        [Required]
        public Guid? MaterialId { get; set; }

        [Required]
        public float Voxels { get; set; }
    }
}
