using System.Collections.Generic;

namespace starbase_nexus_api.Models.Yolol.YololProject.FetchConfig
{
    public class FetchConfigValidationResult
    {
        public Dictionary<string, List<string>> Successes { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();

        public FetchConfig FetchConfig { get; set; }
    }
}
