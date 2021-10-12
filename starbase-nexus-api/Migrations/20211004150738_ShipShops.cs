using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class ShipShops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscordDiscriminator",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Constructions_ShipClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructions_ShipClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InGame_ShipShops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ImageUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Left = table.Column<int>(type: "int", nullable: false),
                    Top = table.Column<int>(type: "int", nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGame_ShipShops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InGame_ShipShopSpots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ShipShopId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Left = table.Column<int>(type: "int", nullable: false),
                    Top = table.Column<int>(type: "int", nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGame_ShipShopSpots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InGame_ShipShopSpots_InGame_ShipShops_ShipShopId",
                        column: x => x.ShipShopId,
                        principalTable: "InGame_ShipShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Constructions_Ships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CompanyId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CreatorId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    ShipClassId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ArmorMaterialId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ShipShopSpotId = table.Column<Guid>(type: "char(36)", nullable: true),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    PreviewImageUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    ImageUris = table.Column<string>(type: "longtext", maxLength: 50000, nullable: true),
                    YoutubeVideoUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    OreCrates = table.Column<int>(type: "int", nullable: true),
                    SpeedWithoutCargo = table.Column<int>(type: "int", nullable: true),
                    SpeedWithCargo = table.Column<int>(type: "int", nullable: true),
                    PriceBlueprint = table.Column<int>(type: "int", nullable: true),
                    PriceShip = table.Column<int>(type: "int", nullable: true),
                    ResourceBridges = table.Column<int>(type: "int", nullable: true),
                    Batteries = table.Column<int>(type: "int", nullable: true),
                    GeneratedPower = table.Column<float>(type: "float", nullable: true),
                    PropellantCapacity = table.Column<int>(type: "int", nullable: true),
                    BackwardThrustPower = table.Column<int>(type: "int", nullable: true),
                    ForwardThrustPower = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<float>(type: "float", nullable: true),
                    Height = table.Column<float>(type: "float", nullable: true),
                    Width = table.Column<float>(type: "float", nullable: true),
                    FlightTime = table.Column<int>(type: "int", nullable: true),
                    TotalMassWithoutCargo = table.Column<float>(type: "float", nullable: true),
                    Radiators = table.Column<int>(type: "int", nullable: true),
                    PrimaryWeaponsAutoCannons = table.Column<int>(type: "int", nullable: true),
                    PrimaryWeaponsLaserCannons = table.Column<int>(type: "int", nullable: true),
                    PrimaryWeaponsPlasmaCannons = table.Column<int>(type: "int", nullable: true),
                    PrimaryWeaponsRailCannons = table.Column<int>(type: "int", nullable: true),
                    PrimaryWeaponsMissileLauncher = table.Column<int>(type: "int", nullable: true),
                    PrimaryWeaponsTorpedoLauncher = table.Column<int>(type: "int", nullable: true),
                    TurretWeaponsAutoCannons = table.Column<int>(type: "int", nullable: true),
                    TurretWeaponsLaserCannons = table.Column<int>(type: "int", nullable: true),
                    TurretWeaponsPlasmaCannons = table.Column<int>(type: "int", nullable: true),
                    TurretWeaponsRailCannons = table.Column<int>(type: "int", nullable: true),
                    TurretWeaponsMissileLauncher = table.Column<int>(type: "int", nullable: true),
                    TurretWeaponsTorpedoLauncher = table.Column<int>(type: "int", nullable: true),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructions_Ships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constructions_Ships_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Constructions_Ships_Constructions_ShipClasses_ShipClassId",
                        column: x => x.ShipClassId,
                        principalTable: "Constructions_ShipClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Constructions_Ships_InGame_Materials_ArmorMaterialId",
                        column: x => x.ArmorMaterialId,
                        principalTable: "InGame_Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Constructions_Ships_InGame_ShipShopSpots_ShipShopSpotId",
                        column: x => x.ShipShopSpotId,
                        principalTable: "InGame_ShipShopSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Constructions_Ships_Social_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Social_Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Constructions_ShipMaterialCosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ShipId = table.Column<Guid>(type: "char(36)", nullable: false),
                    MaterialId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Voxels = table.Column<float>(type: "float", nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constructions_ShipMaterialCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constructions_ShipMaterialCosts_Constructions_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Constructions_Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Constructions_ShipMaterialCosts_InGame_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "InGame_Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Social_Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Stars = table.Column<uint>(type: "int unsigned", nullable: false),
                    ShipId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Comment = table.Column<string>(type: "longtext", maxLength: 50000, nullable: true),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Social_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Social_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Social_Ratings_Constructions_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Constructions_Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_ShipMaterialCosts_MaterialId",
                table: "Constructions_ShipMaterialCosts",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_ShipMaterialCosts_ShipId",
                table: "Constructions_ShipMaterialCosts",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_Ships_ArmorMaterialId",
                table: "Constructions_Ships",
                column: "ArmorMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_Ships_CompanyId",
                table: "Constructions_Ships",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_Ships_CreatorId",
                table: "Constructions_Ships",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_Ships_ShipClassId",
                table: "Constructions_Ships",
                column: "ShipClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Constructions_Ships_ShipShopSpotId",
                table: "Constructions_Ships",
                column: "ShipShopSpotId");

            migrationBuilder.CreateIndex(
                name: "IX_InGame_ShipShopSpots_ShipShopId",
                table: "InGame_ShipShopSpots",
                column: "ShipShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Social_Ratings_ShipId",
                table: "Social_Ratings",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Social_Ratings_UserId",
                table: "Social_Ratings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Constructions_ShipMaterialCosts");

            migrationBuilder.DropTable(
                name: "Social_Ratings");

            migrationBuilder.DropTable(
                name: "Constructions_Ships");

            migrationBuilder.DropTable(
                name: "Constructions_ShipClasses");

            migrationBuilder.DropTable(
                name: "InGame_ShipShopSpots");

            migrationBuilder.DropTable(
                name: "InGame_ShipShops");

            migrationBuilder.DropColumn(
                name: "DiscordDiscriminator",
                table: "AspNetUsers");
        }
    }
}
