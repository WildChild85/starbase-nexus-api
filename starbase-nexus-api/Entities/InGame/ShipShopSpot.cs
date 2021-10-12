using starbase_nexus_api.Entities.Constructions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace starbase_nexus_api.Entities.InGame
{
    public class ShipShopSpot : UuidBaseEntity
    {
        [Required]
        [ForeignKey("ShipShopId")]
        public Guid ShipShopId { get; set; }
        [Required]
        public virtual ShipShop ShipShop { get; set; }

        [Required]
        [ForeignKey("ShipId")]
        public Guid? ShipId { get; set; }
        [Required]
        public virtual Ship? Ship { get; set; }

        public int Position { get; set; }

        public int Size { get; set; } = 1;

        public int? Height { get; set; }

        public int? Width { get; set; }

        public int? Left { get; set; }

        public int? Top { get; set; }
    }
}
