using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class MakeShipIdNullabeInShipShopSpot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ShipId",
                table: "InGame_ShipShopSpots",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ShipId",
                table: "InGame_ShipShopSpots",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);
        }
    }
}
