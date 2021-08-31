using System;

namespace starbase_nexus_api.Models.InGame.Material
{
    public class ViewMaterial : UuidViewModel
    {
        public string Description { get; set; }

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

        public Guid MaterialCategoryId { get; set; }
    }
}
