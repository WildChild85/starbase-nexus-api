using System;

namespace starbase_nexus_api.Models.Social.Like
{
    public class ViewLike : UuidViewModel
    {
        public string UserId { get; set; }

        public Guid? YololProjectId { get; set; }
    }
}
