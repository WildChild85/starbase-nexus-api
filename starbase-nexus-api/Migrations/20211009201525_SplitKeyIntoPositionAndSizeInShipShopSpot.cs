using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class SplitKeyIntoPositionAndSizeInShipShopSpot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "InGame_ShipShopSpots");

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "InGame_ShipShopSpots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "InGame_ShipShopSpots",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "InGame_ShipShopSpots");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "InGame_ShipShopSpots");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "InGame_ShipShopSpots",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
