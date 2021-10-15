using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class MakeFlightTimeToFloatInShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "FlightTime",
                table: "Constructions_Ships",
                type: "float",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FlightTime",
                table: "Constructions_Ships",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);
        }
    }
}
