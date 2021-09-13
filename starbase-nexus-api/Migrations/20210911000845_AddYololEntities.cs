using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class AddYololEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Yolol_YololProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Documentation = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    CreatorId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    PreviewImageUri = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yolol_YololProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yolol_YololProjects_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Yolol_YololScripts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Code = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    ProjectId = table.Column<Guid>(type: "char(36)", nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yolol_YololScripts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yolol_YololScripts_Yolol_YololProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Yolol_YololProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Yolol_YololProjects_CreatorId",
                table: "Yolol_YololProjects",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Yolol_YololScripts_ProjectId",
                table: "Yolol_YololScripts",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Yolol_YololScripts");

            migrationBuilder.DropTable(
                name: "Yolol_YololProjects");
        }
    }
}
