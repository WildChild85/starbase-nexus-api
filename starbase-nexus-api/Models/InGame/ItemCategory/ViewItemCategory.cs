using System;

namespace starbase_nexus_api.Models.InGame.ItemCategory
{
    public class ViewItemCategory : UuidViewModel
    {
        public string? Description { get; set; }

        public string Name { get; set; }

        public Guid? ParentId { get; set; }
    }
}
