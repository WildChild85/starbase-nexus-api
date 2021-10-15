namespace starbase_nexus_api.Models.InGame.ShipShop
{
    public class ViewShipShop : UuidViewModel
    {
        public string? ModeratorId { get; set; }

        public string? ImageUri { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Layout { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public int? Left { get; set; }

        public int? Top { get; set; }
    }
}
