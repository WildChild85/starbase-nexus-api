using starbase_nexus_api.Models.Api;

namespace starbase_nexus_api.Models.InGame.Material
{
    public class MaterialSearchParameters : SearchParameters
    {
        public string MaterialCategoryIds { get; set; } = null;
    }
}
