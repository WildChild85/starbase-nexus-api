using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class MoveShipShopSpotShipRefToShipShopSpot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Constructions_Ships_InGame_ShipShopSpots_ShipShopSpotId",
                table: "Constructions_Ships");

            migrationBuilder.DropIndex(
                name: "IX_Constructions_Ships_ShipShopSpotId",
                table: "Constructions_Ships");

            migrationBuilder.DropColumn(
                name: "ShipShopSpotId",
                table: "Constructions_Ships");

            migrationBuilder.AddColumn<Guid>(
                name: "ShipId",
                table: "InGame_ShipShopSpots",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InGame_ShipShopSpots_ShipId",
                table: "InGame_ShipShopSpots",
                column: "ShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_InGame_ShipShopSpots_Constructions_Ships_ShipId",
                table: "InGame_ShipShopSpots",
                column: "ShipId",
                principalTable: "Constructions_Ships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InGame_ShipShopSpots_Constructions_Ships_ShipId",
                table: "InGame_ShipShopSpots");

            migrationBuilder.DropIndex(
                name: "IX_InGame_ShipShopSpots_ShipId",
                table: "InGame_ShipShopSpots");

            migrationBuilder.DropColumn(
                name: "ShipId",
                table: "InGame_ShipShopSpots");

            migrationBuilder.AddColumn<Guid>(
                name: "ShipShopSpotId",
                table: "Constructions_Ships",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_Ships_ShipShopSpotId",
                table: "Constructions_Ships",
                column: "ShipShopSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Constructions_Ships_InGame_ShipShopSpots_ShipShopSpotId",
                table: "Constructions_Ships",
                column: "ShipShopSpotId",
                principalTable: "InGame_ShipShopSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
