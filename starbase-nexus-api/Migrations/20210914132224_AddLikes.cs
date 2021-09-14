using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class AddLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Social_Likes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    YololProjectId = table.Column<Guid>(type: "char(36)", nullable: true),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Social_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Social_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Social_Likes_Yolol_YololProjects_YololProjectId",
                        column: x => x.YololProjectId,
                        principalTable: "Yolol_YololProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Social_Likes_UserId",
                table: "Social_Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Social_Likes_YololProjectId",
                table: "Social_Likes",
                column: "YololProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Social_Likes");
        }
    }
}
