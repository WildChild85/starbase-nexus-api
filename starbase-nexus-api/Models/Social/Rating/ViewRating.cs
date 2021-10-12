using System;

namespace starbase_nexus_api.Models.Social.Rating
{
    public class ViewRating : UuidViewModel
    {
        public uint Stars { get; set; }

        public Guid ShipId { get; set; }

        public string UserId { get; set; }

        public string? Comment { get; set; }
    }
}
