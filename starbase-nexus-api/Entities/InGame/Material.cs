using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.InGame
{
    public class Material : UuidBaseEntity
    {
        [Required]
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string Description { get; set; }

        [Required]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? IconUri { get; set; }

        [ForeignKey("MaterialCategoryId")]
        public Guid MaterialCategoryId { get; set; }
        public virtual MaterialCategory MaterialCategory { get; set; }

        /// <summary>
        /// per kv
        /// </summary>
        public double? Armor { get; set; }

        /// <summary>
        /// per kv
        /// </summary>
        public double? MinArmor { get; set; }

        public double? VoxelPenetrationMultiplier { get; set; }

        public double? CorrosionResistance { get; set; }

        public double? Transformability { get; set; }

        public double? StructuralDurability { get; set; }

        /// <summary>
        /// kv/kg
        /// </summary>
        public double? Density { get; set; }

        /// <summary>
        /// kv/kg
        /// </summary>
        public double? OreDensity { get; set; }

        public string? IconUriRaw { get; set; }

        public string? IconUriRefined { get; set; }

        public string? IconUriOreChunk { get; set; }
    }
}
