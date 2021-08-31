using starbase_nexus_api.Models.Api;

namespace starbase_nexus_api.Models.InGame.ItemCategory
{
    public class ItemCategorySearchParameters : SearchParameters
    {
        public string? ParentIds { get; set; }
    }
}
