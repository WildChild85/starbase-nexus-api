using starbase_nexus_api.Constants;
using starbase_nexus_api.DbContexts;
using starbase_nexus_api.Entities.Constructions;
using starbase_nexus_api.Helpers;
using starbase_nexus_api.Models.Api;
using starbase_nexus_api.Models.Constructions.Ship;
using starbase_nexus_api.StaticServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Repositories.Constructions
{
    public class ShipRepository : UuidBaseRepository<Ship>, IShipRepository<Ship>
    {
        public ShipRepository(MainDbContext context) : base(context)
        {

        }
        public virtual async Task<PagedList<Ship>> GetMultiple(ShipSearchParameters parameters)
        {
            IQueryable<Ship> collection = _dbSet as IQueryable<Ship>;

            if (parameters.CreatorIds != null)
            {
                List<string> creatorIds = parameters.CreatorIds.Split(',').ToList();
                if (creatorIds.Count > 0)
                {
                    collection = collection.Where(r => r.CreatorId != null && creatorIds.Contains(r.CreatorId));
                }
            }

            if (parameters.CompanyIds != null)
            {
                List<Guid> companyIds = TextService.GetGuidArray(parameters.CompanyIds, ',').ToList();
                if (companyIds.Count > 0)
                {
                    collection = collection.Where(r => r.CompanyId != null && companyIds.Contains((Guid)r.CompanyId));
                }
            }

            if (parameters.ShipClassIds != null)
            {
                List<Guid> shipClassIds = TextService.GetGuidArray(parameters.ShipClassIds, ',').ToList();
                if (shipClassIds.Count > 0)
                {
                    collection = collection.Where(r => shipClassIds.Contains((Guid)r.ShipClassId));
                }
            }

            if (parameters.ArmorMaterialIds != null)
            {
                List<Guid> armorMaterialIds = TextService.GetGuidArray(parameters.ArmorMaterialIds, ',').ToList();
                if (armorMaterialIds.Count > 0)
                {
                    collection = collection.Where(r => r.ArmorMaterialId != null && armorMaterialIds.Contains((Guid)r.ArmorMaterialId));
                }
            }

            if (parameters.MinOreCrates != null)
                collection = collection.Where(r => r.OreCrates != null && r.OreCrates >= parameters.MinOreCrates);

            if (parameters.MaxOreCrates != null)
                collection = collection.Where(r => r.OreCrates != null && r.OreCrates <= parameters.MaxOreCrates);

            if (parameters.MinSpeedWithoutCargo != null)
                collection = collection.Where(r => r.SpeedWithoutCargo != null && r.SpeedWithoutCargo >= parameters.MinSpeedWithoutCargo);

            if (parameters.MaxSpeedWithoutCargo != null)
                collection = collection.Where(r => r.SpeedWithCargo != null && r.SpeedWithoutCargo <= parameters.MaxSpeedWithoutCargo);

            if (parameters.MinSpeedWithCargo != null)
                collection = collection.Where(r => r.SpeedWithCargo != null && r.SpeedWithCargo >= parameters.MinSpeedWithCargo);

            if (parameters.MaxSpeedWithCargo != null)
                collection = collection.Where(r => r.SpeedWithCargo != null && r.SpeedWithCargo <= parameters.MaxSpeedWithCargo);

            if (parameters.MinPriceBlueprint != null)
                collection = collection.Where(r => r.PriceBlueprint != null && r.PriceBlueprint >= parameters.MinPriceBlueprint);

            if (parameters.MaxSpeedWithCargo != null)
                collection = collection.Where(r => r.PriceBlueprint != null && r.PriceBlueprint <= parameters.MaxPriceBlueprint);

            if (parameters.MinPriceShip != null)
                collection = collection.Where(r => r.PriceShip != null && r.PriceShip >= parameters.MinPriceShip);

            if (parameters.MaxPriceShip != null)
                collection = collection.Where(r => r.PriceShip != null && r.PriceShip <= parameters.MaxPriceShip);

            if (parameters.MinResourceBridges != null)
                collection = collection.Where(r => r.ResourceBridges != null && r.ResourceBridges >= parameters.MinResourceBridges);

            if (parameters.MaxResourceBridges != null)
                collection = collection.Where(r => r.ResourceBridges != null && r.ResourceBridges <= parameters.MaxResourceBridges);

            if (parameters.MinBatteries != null)
                collection = collection.Where(r => r.Batteries != null && r.Batteries >= parameters.MinBatteries);

            if (parameters.MaxBatteries != null)
                collection = collection.Where(r => r.Batteries != null && r.Batteries <= parameters.MaxBatteries);

            if (parameters.MinGeneratedPower != null)
                collection = collection.Where(r => r.GeneratedPower != null && r.GeneratedPower >= parameters.MinGeneratedPower);

            if (parameters.MaxGeneratedPower != null)
                collection = collection.Where(r => r.GeneratedPower != null && r.GeneratedPower <= parameters.MaxGeneratedPower);

            if (parameters.MinPropellantCapacity != null)
                collection = collection.Where(r => r.PropellantCapacity != null && r.PropellantCapacity >= parameters.MinPropellantCapacity);

            if (parameters.MaxPropellantCapacity != null)
                collection = collection.Where(r => r.PropellantCapacity != null && r.PropellantCapacity <= parameters.MaxPropellantCapacity);

            if (parameters.MinBackwardThrustPower != null)
                collection = collection.Where(r => r.BackwardThrustPower != null && r.BackwardThrustPower >= parameters.MinBackwardThrustPower);

            if (parameters.MaxBackwardThrustPower != null)
                collection = collection.Where(r => r.BackwardThrustPower != null && r.BackwardThrustPower <= parameters.MaxBackwardThrustPower);

            if (parameters.MinForwardThrustPower != null)
                collection = collection.Where(r => r.ForwardThrustPower != null && r.ForwardThrustPower >= parameters.MinForwardThrustPower);

            if (parameters.MaxForwardThrustPower != null)
                collection = collection.Where(r => r.ForwardThrustPower != null && r.ForwardThrustPower <= parameters.MaxForwardThrustPower);

            if (parameters.MinLength != null)
                collection = collection.Where(r => r.Length != null && r.Length >= parameters.MinLength);

            if (parameters.MaxLength != null)
                collection = collection.Where(r => r.Length != null && r.Length <= parameters.MaxLength);

            if (parameters.MinHeight != null)
                collection = collection.Where(r => r.Height != null && r.Height >= parameters.MinHeight);

            if (parameters.MaxHeight != null)
                collection = collection.Where(r => r.Height != null && r.Height <= parameters.MaxHeight);

            if (parameters.MinWidth != null)
                collection = collection.Where(r => r.Width != null && r.Width >= parameters.MinWidth);

            if (parameters.MaxWidth != null)
                collection = collection.Where(r => r.Width != null && r.Width <= parameters.MaxWidth);

            if (parameters.MinFlightTime != null)
                collection = collection.Where(r => r.FlightTime != null && r.FlightTime >= parameters.MinFlightTime);

            if (parameters.MaxFlightTime != null)
                collection = collection.Where(r => r.FlightTime != null && r.FlightTime <= parameters.MaxFlightTime);

            if (parameters.MinTotalMassWithoutCargo != null)
                collection = collection.Where(r => r.TotalMassWithoutCargo != null && r.TotalMassWithoutCargo >= parameters.MinTotalMassWithoutCargo);

            if (parameters.MaxTotalMassWithoutCargo != null)
                collection = collection.Where(r => r.TotalMassWithoutCargo != null && r.TotalMassWithoutCargo <= parameters.MaxTotalMassWithoutCargo);

            if (parameters.MinRadiators != null)
                collection = collection.Where(r => r.Radiators != null && r.Radiators >= parameters.MinRadiators);

            if (parameters.MaxRadiators != null)
                collection = collection.Where(r => r.Radiators != null && r.Radiators <= parameters.MaxRadiators);

            if (parameters.MinMiningLasers != null)
                collection = collection.Where(r => r.MiningLasers != null && r.MiningLasers >= parameters.MinMiningLasers);

            if (parameters.MaxMiningLasers != null)
                collection = collection.Where(r => r.MiningLasers != null && r.MiningLasers <= parameters.MaxMiningLasers);

            if (parameters.MinOreCollectors != null)
                collection = collection.Where(r => r.OreCollectors != null && r.OreCollectors >= parameters.MinOreCollectors);

            if (parameters.MaxOreCollectors != null)
                collection = collection.Where(r => r.OreCollectors != null && r.OreCollectors <= parameters.MaxOreCollectors);

            if (parameters.MinPrimaryWeaponsAutoCannons != null)
                collection = collection.Where(r => r.PrimaryWeaponsAutoCannons != null && r.PrimaryWeaponsAutoCannons >= parameters.MinPrimaryWeaponsAutoCannons);

            if (parameters.MaxPrimaryWeaponsLaserCannons != null)
                collection = collection.Where(r => r.PrimaryWeaponsLaserCannons != null && r.PrimaryWeaponsLaserCannons <= parameters.MaxPrimaryWeaponsLaserCannons);

            if (parameters.MinPrimaryWeaponsPlasmaCannons != null)
                collection = collection.Where(r => r.PrimaryWeaponsPlasmaCannons != null && r.PrimaryWeaponsPlasmaCannons >= parameters.MinPrimaryWeaponsPlasmaCannons);

            if (parameters.MaxPrimaryWeaponsRailCannons != null)
                collection = collection.Where(r => r.PrimaryWeaponsRailCannons != null && r.PrimaryWeaponsRailCannons <= parameters.MaxPrimaryWeaponsRailCannons);

            if (parameters.MinPrimaryWeaponsMissileLauncher != null)
                collection = collection.Where(r => r.PrimaryWeaponsMissileLauncher != null && r.PrimaryWeaponsMissileLauncher >= parameters.MinPrimaryWeaponsMissileLauncher);

            if (parameters.MaxPrimaryWeaponsMissileLauncher != null)
                collection = collection.Where(r => r.PrimaryWeaponsMissileLauncher != null && r.PrimaryWeaponsMissileLauncher <= parameters.MaxPrimaryWeaponsMissileLauncher);

            if (parameters.MinPrimaryWeaponsTorpedoLauncher != null)
                collection = collection.Where(r => r.PrimaryWeaponsTorpedoLauncher != null && r.PrimaryWeaponsTorpedoLauncher >= parameters.MinPrimaryWeaponsTorpedoLauncher);

            if (parameters.MaxPrimaryWeaponsTorpedoLauncher != null)
                collection = collection.Where(r => r.PrimaryWeaponsTorpedoLauncher != null && r.PrimaryWeaponsTorpedoLauncher <= parameters.MaxPrimaryWeaponsTorpedoLauncher);

            if (parameters.MinTurretWeaponsAutoCannons != null)
                collection = collection.Where(r => r.TurretWeaponsAutoCannons != null && r.TurretWeaponsAutoCannons >= parameters.MinTurretWeaponsAutoCannons);

            if (parameters.MaxTurretWeaponsLaserCannons != null)
                collection = collection.Where(r => r.TurretWeaponsLaserCannons != null && r.TurretWeaponsLaserCannons <= parameters.MaxTurretWeaponsLaserCannons);

            if (parameters.MinTurretWeaponsPlasmaCannons != null)
                collection = collection.Where(r => r.TurretWeaponsPlasmaCannons != null && r.TurretWeaponsPlasmaCannons >= parameters.MinTurretWeaponsPlasmaCannons);

            if (parameters.MaxTurretWeaponsRailCannons != null)
                collection = collection.Where(r => r.TurretWeaponsRailCannons != null && r.TurretWeaponsRailCannons <= parameters.MaxTurretWeaponsRailCannons);

            if (parameters.MinTurretWeaponsMissileLauncher != null)
                collection = collection.Where(r => r.TurretWeaponsMissileLauncher != null && r.TurretWeaponsMissileLauncher >= parameters.MinTurretWeaponsMissileLauncher);

            if (parameters.MaxTurretWeaponsMissileLauncher != null)
                collection = collection.Where(r => r.TurretWeaponsMissileLauncher != null && r.TurretWeaponsMissileLauncher <= parameters.MaxTurretWeaponsMissileLauncher);

            if (parameters.MinTurretWeaponsTorpedoLauncher != null)
                collection = collection.Where(r => r.TurretWeaponsTorpedoLauncher != null && r.TurretWeaponsTorpedoLauncher >= parameters.MinTurretWeaponsTorpedoLauncher);

            if (parameters.MaxTurretWeaponsTorpedoLauncher != null)
                collection = collection.Where(r => r.TurretWeaponsTorpedoLauncher != null && r.TurretWeaponsTorpedoLauncher <= parameters.MaxTurretWeaponsTorpedoLauncher);



            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<Ship> pagedList = await PagedList<Ship>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
