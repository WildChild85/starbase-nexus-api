using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class YololProjectFetchConfigToFetchConfigUri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FetchConfig",
                table: "Yolol_YololProjects");

            migrationBuilder.AddColumn<string>(
                name: "FetchConfigUri",
                table: "Yolol_YololProjects",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FetchConfigUri",
                table: "Yolol_YololProjects");

            migrationBuilder.AddColumn<string>(
                name: "FetchConfig",
                table: "Yolol_YololProjects",
                type: "longtext",
                maxLength: 50000,
                nullable: true);
        }
    }
}
