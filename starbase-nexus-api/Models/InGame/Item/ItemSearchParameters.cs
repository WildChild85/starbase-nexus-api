using starbase_nexus_api.Models.Api;

namespace starbase_nexus_api.Models.InGame.Item
{
    public class ItemSearchParameters : SearchParameters
    {
        public string? ItemCategoryIds { get; set; }

        public string? PrimaryMaterialIds { get; set; }
    }
}
