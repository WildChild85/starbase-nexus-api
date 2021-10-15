using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class MakeShipShopDescriptionMultiline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InGame_ShipShops",
                type: "longtext",
                maxLength: 50000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InGame_ShipShops",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 50000);
        }
    }
}
