using starbase_nexus_api.Constants;
using starbase_nexus_api.Entities.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.Authentication
{
    public class RefreshToken
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Token { get; set; }

        public DateTimeOffset ExpiresAt { get; set; }

        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? IpAddress { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
