using starbase_nexus_api.Models.Api;

namespace starbase_nexus_api.Models.Constructions.Ship
{
    public class ShipSearchParameters : SearchParameters
    {
        public string? CompanyIds { get; set; }

        public string? CreatorIds { get; set; }

        public string? ShipClassIds { get; set; }

        public string? ArmorMaterialIds { get; set; }

        public int? MinOreCrates { get; set; }
        public int? MaxOreCrates { get; set; }

        public int? MinSpeedWithoutCargo { get; set; }
        public int? MaxSpeedWithoutCargo { get; set; }

        public int? MinSpeedWithCargo { get; set; }
        public int? MaxSpeedWithCargo { get; set; }

        public int? MinPriceBlueprint { get; set; }
        public int? MaxPriceBlueprint { get; set; }

        public int? MinPriceShip { get; set; }
        public int? MaxPriceShip { get; set; }

        public int? MinResourceBridges { get; set; }
        public int? MaxResourceBridges { get; set; }

        public int? MinBatteries { get; set; }
        public int? MaxBatteries { get; set; }

        /// <summary>
        /// e/s
        /// </summary>
        public float? MinGeneratedPower { get; set; }
        public float? MaxGeneratedPower { get; set; }

        public int? MinPropellantCapacity { get; set; }
        public int? MaxPropellantCapacity { get; set; }

        public int? MinBackwardThrustPower { get; set; }
        public int? MaxBackwardThrustPower { get; set; }

        public int? MinForwardThrustPower { get; set; }
        public int? MaxForwardThrustPower { get; set; }

        /// <summary>
        /// m
        /// </summary>
        public float? MinLength { get; set; }
        public float? MaxLength { get; set; }

        /// <summary>
        /// m
        /// </summary>
        public float? MinHeight { get; set; }
        public float? MaxHeight { get; set; }

        /// <summary>
        /// m
        /// </summary>
        public float? MinWidth { get; set; }
        public float? MaxWidth { get; set; }

        /// <summary>
        /// in minutes
        /// </summary>
        public int? MinFlightTime { get; set; }
        public int? MaxFlightTime { get; set; }

        public float? MinTotalMassWithoutCargo { get; set; }
        public float? MaxTotalMassWithoutCargo { get; set; }

        /// <summary>
        /// with extensions
        /// </summary>
        public int? MinRadiators { get; set; }
        public int? MaxRadiators { get; set; }

        public int? MinMiningLasers { get; set; }
        public int? MaxMiningLasers { get; set; }

        public int? MinOreCollectors { get; set; }
        public int? MaxOreCollectors { get; set; }

        public int? MinPrimaryWeaponsAutoCannons { get; set; }
        public int? MaxPrimaryWeaponsAutoCannons { get; set; }

        public int? MinPrimaryWeaponsLaserCannons { get; set; }
        public int? MaxPrimaryWeaponsLaserCannons { get; set; }

        public int? MinPrimaryWeaponsPlasmaCannons { get; set; }
        public int? MaxPrimaryWeaponsPlasmaCannons { get; set; }

        public int? MinPrimaryWeaponsRailCannons { get; set; }
        public int? MaxPrimaryWeaponsRailCannons { get; set; }

        public int? MinPrimaryWeaponsMissileLauncher { get; set; }
        public int? MaxPrimaryWeaponsMissileLauncher { get; set; }

        public int? MinPrimaryWeaponsTorpedoLauncher { get; set; }
        public int? MaxPrimaryWeaponsTorpedoLauncher { get; set; }

        public int? MinTurretWeaponsAutoCannons { get; set; }
        public int? MaxTurretWeaponsAutoCannons { get; set; }

        public int? MinTurretWeaponsLaserCannons { get; set; }
        public int? MaxTurretWeaponsLaserCannons { get; set; }

        public int? MinTurretWeaponsPlasmaCannons { get; set; }
        public int? MaxTurretWeaponsPlasmaCannons { get; set; }

        public int? MinTurretWeaponsRailCannons { get; set; }
        public int? MaxTurretWeaponsRailCannons { get; set; }

        public int? MinTurretWeaponsMissileLauncher { get; set; }
        public int? MaxTurretWeaponsMissileLauncher { get; set; }

        public int? MinTurretWeaponsTorpedoLauncher { get; set; }
        public int? MaxTurretWeaponsTorpedoLauncher { get; set; }
    }
}
