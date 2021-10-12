using System;

namespace starbase_nexus_api.Models.InGame.ShipShopSpot
{
    public class ViewShipShopSpot : UuidViewModel
    {
        public Guid ShipShopId { get; set; }

        public Guid? ShipId { get; set; }

        public int Position { get; set; }

        public int Size { get; set; } = 1;

        public int? Height { get; set; }

        public int? Width { get; set; }

        public int? Left { get; set; }

        public int? Top { get; set; }
    }
}
