using System.Collections.Generic;

namespace starbase_nexus_api.Models.Yolol.YololProject.Fetched
{
    public class FetchedYololProject
    {
        public string Documentation { get; set; }

        public List<FetchedYololScript> Scripts { get; set; } = new List<FetchedYololScript>();
    }
}
