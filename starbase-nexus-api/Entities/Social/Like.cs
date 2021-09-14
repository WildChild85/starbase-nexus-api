using starbase_nexus_api.Constants;
using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Entities.Yolol;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.Social
{
    public class Like : UuidBaseEntity
    {
        [MaxLength(InputSizes.GUID_MAX_LENGTH)]
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("ProjectYololId")]
        public Guid? YololProjectId { get; set; }
        public virtual YololProject? YololProject { get; set; }
    }
}
