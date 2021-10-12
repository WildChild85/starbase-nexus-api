using starbase_nexus_api.Constants;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Entities.Constructions
{
    public class ShipRole : UuidBaseEntity
    {
        [Required]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }
    }
}
