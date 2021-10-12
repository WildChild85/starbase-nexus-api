using starbase_nexus_api.Models.Api;

namespace starbase_nexus_api.Models.InGame.ShipShopSpot
{
    public class ShipShopSpotSearchParameters : SearchParameters
    {
        public string? ShipShopIds { get; set; }

        public string? ShipIds { get; set; }

        public int? Position { get; set; }
    }
}
