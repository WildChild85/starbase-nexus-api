using starbase_nexus_api.Entities.Identity;
using starbase_nexus_api.Entities.Social;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.Contructions
{
    public class Ship : UuidBaseEntity
    {
        [ForeignKey("CompanyId")]
        public Guid? CompanyId { get; set; }
        public virtual Company? Company { get; set; }

        [ForeignKey("OwnerId")]
        public string? OwnerId { get; set; }
        public virtual User? Owner { get; set; }

        [ForeignKey("ShipClassId")]
        public Guid ShipClassId { get; set; }
        public virtual ShipClass ShipClass { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? PreviewImageUri { get; set; }

        public string? ImageUris { get; set; }

        public string? YoutubeVideoUri { get; set; }

        public int? OreCrates { get; set; }

        public int? MaxSpeedWithoutCargo { get; set; }

        public int? MaxSpeedWithCargo { get; set; }

        public int? PriceBlueprint { get; set; }

        public int? PriceShip { get; set; }

        public int? ResourceBridges { get; set; }

        public int? Batteries { get; set; }

        public float? GeneratedPower { get; set; }

        public int? MaxPropellant { get; set; }

        public int? MaxBackwardThrustPower { get; set; }

        public int? MaxForwardThrustPower { get; set; }

        public float? Length { get; set; }

        public float? Height { get; set; }

        public float? Width { get; set; }

        public int? MaxFlightTime { get; set; }

        public float? TotalMassWithoutCargo { get; set; }

        public uint? Radiators { get; set; }

        public bool HasPrimaryWeapons { get; set; } = false;

        public uint? WeaponTurrets { get; set; }
    }
}
