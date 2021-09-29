using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class AddCoolantInputAndOutToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CoolantInput",
                table: "InGame_Items",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CoolantOutput",
                table: "InGame_Items",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoolantInput",
                table: "InGame_Items");

            migrationBuilder.DropColumn(
                name: "CoolantOutput",
                table: "InGame_Items");
        }
    }
}
