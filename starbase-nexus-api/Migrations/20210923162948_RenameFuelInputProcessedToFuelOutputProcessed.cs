using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class RenameFuelInputProcessedToFuelOutputProcessed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FuelInputProcessed",
                table: "InGame_Items",
                newName: "FuelOutputProcessed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FuelOutputProcessed",
                table: "InGame_Items",
                newName: "FuelInputProcessed");
        }
    }
}
