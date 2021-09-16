using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class AddCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Social_Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    AboutUs = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    LogoUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    DiscordUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    WebsiteUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    YoutubeUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    TwitchUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    CreatorId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Social_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Social_Companies_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Social_Companies_CreatorId",
                table: "Social_Companies",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Social_Companies");
        }
    }
}
