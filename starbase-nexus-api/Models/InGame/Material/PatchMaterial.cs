using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.InGame.Material
{
    public class PatchMaterial
    {
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string Description { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

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
        /// kg/kv
        /// </summary>
        public double? Density { get; set; }

        /// <summary>
        /// kg/kg
        /// </summary>
        public double? OreDensity { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? IconUriRaw { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? IconUriRefined { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? IconUriOreChunk { get; set; }

        public Guid MaterialCategoryId { get; set; }
    }
}
