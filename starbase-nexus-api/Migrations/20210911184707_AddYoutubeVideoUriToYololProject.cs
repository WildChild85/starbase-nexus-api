using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class AddYoutubeVideoUriToYololProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PreviewImageUri",
                table: "Yolol_YololProjects",
                type: "longtext",
                maxLength: 50000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 50000);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeVideoUri",
                table: "Yolol_YololProjects",
                type: "longtext",
                maxLength: 50000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YoutubeVideoUri",
                table: "Yolol_YololProjects");

            migrationBuilder.AlterColumn<string>(
                name: "PreviewImageUri",
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
