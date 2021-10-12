using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class RenameShipClassToRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Constructions_Ships_Constructions_ShipClasses_ShipClassId",
                table: "Constructions_Ships");

            migrationBuilder.DropTable(
                name: "Constructions_ShipClasses");

            migrationBuilder.DropIndex(
                name: "IX_Constructions_Ships_ShipClassId",
                table: "Constructions_Ships");

            migrationBuilder.DropColumn(
                name: "ShipClassId",
                table: "Constructions_Ships");

            migrationBuilder.CreateTable(
                name: "Constructions_ShipRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructions_ShipRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constructions_ShipRoleReference",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ShipId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ShipRoleId = table.Column<Guid>(type: "char(36)", nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructions_ShipRoleReference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constructions_ShipRoleReference_Constructions_ShipRoles_Ship~",
                        column: x => x.ShipRoleId,
                        principalTable: "Constructions_ShipRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Constructions_ShipRoleReference_Constructions_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Constructions_Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_ShipRoleReference_ShipId",
                table: "Constructions_ShipRoleReference",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_ShipRoleReference_ShipRoleId",
                table: "Constructions_ShipRoleReference",
                column: "ShipRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Constructions_ShipRoleReference");

            migrationBuilder.DropTable(
                name: "Constructions_ShipRoles");

            migrationBuilder.AddColumn<Guid>(
                name: "ShipClassId",
                table: "Constructions_Ships",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "Constructions_ShipClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructions_ShipClasses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_Ships_ShipClassId",
                table: "Constructions_Ships",
                column: "ShipClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Constructions_Ships_Constructions_ShipClasses_ShipClassId",
                table: "Constructions_Ships",
                column: "ShipClassId",
                principalTable: "Constructions_ShipClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
