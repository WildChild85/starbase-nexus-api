using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class AddModeratorToShipShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModeratorId",
                table: "InGame_ShipShops",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InGame_ShipShops_ModeratorId",
                table: "InGame_ShipShops",
                column: "ModeratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_InGame_ShipShops_AspNetUsers_ModeratorId",
                table: "InGame_ShipShops",
                column: "ModeratorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InGame_ShipShops_AspNetUsers_ModeratorId",
                table: "InGame_ShipShops");

            migrationBuilder.DropIndex(
                name: "IX_InGame_ShipShops_ModeratorId",
                table: "InGame_ShipShops");

            migrationBuilder.DropColumn(
                name: "ModeratorId",
                table: "InGame_ShipShops");
        }
    }
}
