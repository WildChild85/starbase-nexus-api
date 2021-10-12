using System;

namespace starbase_nexus_api.Models.Constructions.ShipMaterialCost
{
    public class ViewShipMaterialCost : UuidViewModel
    {
        public Guid ShipId { get; set; }

        public Guid MaterialId { get; set; }

        public float Voxels { get; set; }
    }
}
