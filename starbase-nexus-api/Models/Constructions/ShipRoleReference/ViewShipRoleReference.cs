using System;

namespace starbase_nexus_api.Models.Constructions.ShipRoleReference
{
    public class ViewShipRoleReference : UuidViewModel
    {
        public Guid ShipId { get; set; }

        public Guid ShipRoleId { get; set; }
    }
}
