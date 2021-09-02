using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class MakeItemCategoryDescriptionOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InGame_ItemCategories",
                type: "longtext",
                maxLength: 50000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldMaxLength: 50000);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "InGame_ItemCategories",
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
