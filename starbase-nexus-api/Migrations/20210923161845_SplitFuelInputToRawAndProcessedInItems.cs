using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class SplitFuelInputToRawAndProcessedInItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FuelInput",
                table: "InGame_Items",
                newName: "FuelInputRaw");

            migrationBuilder.AddColumn<float>(
                name: "FuelInputProcessed",
                table: "InGame_Items",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuelInputProcessed",
                table: "InGame_Items");

            migrationBuilder.RenameColumn(
                name: "FuelInputRaw",
                table: "InGame_Items",
                newName: "FuelInput");
        }
    }
}
