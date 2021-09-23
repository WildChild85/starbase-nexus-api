using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class AddGuideIdToLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GuideId",
                table: "Social_Likes",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Social_Likes_GuideId",
                table: "Social_Likes",
                column: "GuideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Social_Likes_Knowledge_Guides_GuideId",
                table: "Social_Likes",
                column: "GuideId",
                principalTable: "Knowledge_Guides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Social_Likes_Knowledge_Guides_GuideId",
                table: "Social_Likes");

            migrationBuilder.DropIndex(
                name: "IX_Social_Likes_GuideId",
                table: "Social_Likes");

            migrationBuilder.DropColumn(
                name: "GuideId",
                table: "Social_Likes");
        }
    }
}
