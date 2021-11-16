using System;

namespace starbase_nexus_api.Models.Yolol.YololScript
{
    public class ViewYololScript : UuidViewModel
    {
        public string? Name { get; set; }

        public string Code { get; set; }

        public Guid ProjectId { get; set; }
    }
}
