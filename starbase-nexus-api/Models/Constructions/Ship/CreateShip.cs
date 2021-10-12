using starbase_nexus_api.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace starbase_nexus_api.Models.Constructions.Ship
{
    public class CreateShip
    {
        public Guid? CompanyId { get; set; }

        [Required]
        public Guid? ShipClassId { get; set; }

        public Guid? ArmorMaterialId { get; set; }

        [Required]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string Description { get; set; }

        [Required]
        [Url]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string PreviewImageUri { get; set; }

        [MinLength(InputSizes.MULTILINE_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? ImageUris { get; set; }

        [Url]
        [MinLength(InputSizes.DEFAULT_TEXT_MIN_LENGTH)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? YoutubeVideoUri { get; set; }

        public int? OreCrates { get; set; }

        public int? SpeedWithoutCargo { get; set; }

        public int? SpeedWithCargo { get; set; }

        public double? PriceBlueprint { get; set; }

        public double? PriceShip { get; set; }

        public int? ResourceBridges { get; set; }

        public int? Batteries { get; set; }

        /// <summary>
        /// e/s
        /// </summary>
        public float? GeneratedPower { get; set; }

        public int? PropellantCapacity { get; set; }

        public int? BackwardThrustPower { get; set; }

        public int? ForwardThrustPower { get; set; }

        /// <summary>
        /// m
        /// </summary>
        public float? Length { get; set; }

        /// <summary>
        /// m
        /// </summary>
        public float? Height { get; set; }

        /// <summary>
        /// m
        /// </summary>
        public float? Width { get; set; }

        /// <summary>
        /// in minutes
        /// </summary>
        public int? FlightTime { get; set; }

        public float? TotalMassWithoutCargo { get; set; }

        /// <summary>
        /// with extensions
        /// </summary>
        public int? Radiators { get; set; }

        public int? MiningLasers { get; set; }

        public int? OreCollectors { get; set; }

        public int? PrimaryWeaponsAutoCannons { get; set; }

        public int? PrimaryWeaponsLaserCannons { get; set; }

        public int? PrimaryWeaponsPlasmaCannons { get; set; }

        public int? PrimaryWeaponsRailCannons { get; set; }

        public int? PrimaryWeaponsMissileLauncher { get; set; }

        public int? PrimaryWeaponsTorpedoLauncher { get; set; }

        public int? TurretWeaponsAutoCannons { get; set; }

        public int? TurretWeaponsLaserCannons { get; set; }

        public int? TurretWeaponsPlasmaCannons { get; set; }

        public int? TurretWeaponsRailCannons { get; set; }

        public int? TurretWeaponsMissileLauncher { get; set; }

        public int? TurretWeaponsTorpedoLauncher { get; set; }

        public bool IsCreator { get; set; } = false;
    }
}
