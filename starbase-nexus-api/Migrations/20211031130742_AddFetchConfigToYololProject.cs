using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class AddFetchConfigToYololProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Yolol_YololScripts",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Documentation",
                table: "Yolol_YololProjects",
                type: "longtext",
                maxLength: 50000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 50000);

            migrationBuilder.AddColumn<string>(
                name: "FetchConfig",
                table: "Yolol_YololProjects",
                type: "longtext",
                maxLength: 50000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Yolol_YololScripts");

            migrationBuilder.DropColumn(
                name: "FetchConfig",
                table: "Yolol_YololProjects");

            migrationBuilder.AlterColumn<string>(
                name: "Documentation",
                table: "Yolol_YololProjects",
                type: "longtext",
                maxLength: 50000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 50000,
                oldNullable: true);
        }
    }
}
