using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class AddMiningLasersAndOreCollectorsToShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MiningLasers",
                table: "Constructions_Ships",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OreCollectors",
                table: "Constructions_Ships",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiningLasers",
                table: "Constructions_Ships");

            migrationBuilder.DropColumn(
                name: "OreCollectors",
                table: "Constructions_Ships");
        }
    }
}
