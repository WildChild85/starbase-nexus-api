using starbase_nexus_api.Models.Api;

namespace starbase_nexus_api.Models.Constructions.ShipMaterialCost
{
    public class ShipMaterialCostSearchParameters : SearchParameters
    {
        public string? ShipIds { get; set; }

        public string? MaterialIds { get; set; }
    }
}
