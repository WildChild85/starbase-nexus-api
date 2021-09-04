using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.InGame
{
    public class Item : UuidBaseEntity
    {
        [Required]
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? Description { get; set; }

        [Required]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? IconUri { get; set; }

        [Required]
        [ForeignKey("ItemCategoryId")]
        public Guid ItemCategoryId { get; set; }
        [Required]
        public virtual ItemCategory ItemCategory { get; set; }

        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? WikiUri { get; set; }

        /// <summary>
        /// kg
        /// </summary>
        public float? Mass { get; set; }

        public float? CorrosionResistance { get; set; }

        [ForeignKey("PrimaryMaterialId")]
        public Guid? PrimaryMaterialId { get; set; }
        public virtual Material? PrimaryMaterial { get; set; }

        /// <summary>
        /// e/s
        /// </summary>
        public float? ElectricInput { get; set; }

        /// <summary>
        /// e/s
        /// </summary>
        public float? ElectricOutput{ get; set; }

        public float? ElectricityConversionBonusFactor { get; set; }

        public float? ElectricCapacity { get; set; }

        /// <summary>
        /// p/s
        /// </summary>
        public float? PropellantInput { get; set; }

        /// <summary>
        /// p/s
        /// </summary>
        public float? PropellantOutput { get; set; }

        public float? PropellantConversionBonusFactor { get; set; }

        public float? PropellantCapacity { get; set; }

        public float? ThrustPower { get; set; }

        public int? Tier { get; set; }

        public float? FuelCapacity { get; set; }

        /// <summary>
        /// f/s
        /// </summary>
        public float? FuelInput { get; set; }

        /// <summary>
        /// h/s
        /// </summary>
        public float? HeatGeneration { get; set; }

        public float? ElectricityPerShot { get; set; }

        public float? ElectricityPerRecharge { get; set; }

        public float? HeatGenerationPerShot { get; set; }

        /// <summary>
        /// h/s
        /// </summary>
        public float? HeatDissipation { get; set; }

        /// <summary>
        /// m/s
        /// </summary>
        public float? MinMuzzleVelocity { get; set; }

        /// <summary>
        /// m/s
        /// </summary>
        public float? MaxMuzzleVelocity { get; set; }

        /// <summary>
        /// rpm
        /// </summary>
        public float? RateOfFire { get; set; }

        /// <summary>
        /// can be smaller than the actual magazine capacity (lasers)
        /// </summary>
        public float? ChargeCapacity { get; set; }

        public float? MagazineCapacity { get; set; }

        /// <summary>
        /// voxels
        /// </summary>
        public float? ProjectileMass { get; set; }

        public float? ProjectileEnergy { get; set; }

        /// <summary>
        /// per/s
        /// </summary>
        public float? ProjectileLifetime { get; set; }

        /// <summary>
        /// in seconds
        /// </summary>
        public float? WarmupTime { get; set; }

        public float? ResearchPointsCube { get; set; }

        public float? ResearchPointsPower { get; set; }

        public float? ResearchPointsShield { get; set; }

        public float? ResearchPointsGear { get; set; }
    }
}
