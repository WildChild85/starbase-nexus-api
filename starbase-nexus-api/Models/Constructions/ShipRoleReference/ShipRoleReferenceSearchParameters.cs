using starbase_nexus_api.Models.Api;

namespace starbase_nexus_api.Models.Constructions.ShipRoleReference
{
    public class ShipRoleReferenceSearchParameters : SearchParameters
    {
        public string? ShipIds { get; set; }

        public string? ShipRoleIds { get; set; }
    }
}
