using System.Collections.Generic;

namespace starbase_nexus_api.Models.Cdn
{
    public class FolderContentResponse
    {
        public List<string> FolderNames { get; set; }

        public List<FileResponse> Files { get; set; }
    }
}
