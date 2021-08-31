using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace starbase_nexus_api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    AboutMe = table.Column<string>(type: "longtext", maxLength: 50000, nullable: true),
                    AvatarUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    DiscordId = table.Column<string>(type: "varchar(255)", nullable: true),
                    LastLogin = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InGame_ItemCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Description = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    ParentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGame_ItemCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InGame_ItemCategories_InGame_ItemCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "InGame_ItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InGame_MaterialCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Description = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGame_MaterialCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authentication_RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Token = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    IpAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authentication_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authentication_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InGame_Materials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Description = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IconUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    MaterialCategoryId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Armor = table.Column<double>(type: "double", nullable: true),
                    MinArmor = table.Column<double>(type: "double", nullable: true),
                    VoxelPenetrationMultiplier = table.Column<double>(type: "double", nullable: true),
                    CorrosionResistance = table.Column<double>(type: "double", nullable: true),
                    Transformability = table.Column<double>(type: "double", nullable: true),
                    StructuralDurability = table.Column<double>(type: "double", nullable: true),
                    Density = table.Column<double>(type: "double", nullable: true),
                    OreDensity = table.Column<double>(type: "double", nullable: true),
                    IconUriRaw = table.Column<string>(type: "longtext", nullable: true),
                    IconUriRefined = table.Column<string>(type: "longtext", nullable: true),
                    IconUriOreChunk = table.Column<string>(type: "longtext", nullable: true),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGame_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InGame_Materials_InGame_MaterialCategories_MaterialCategoryId",
                        column: x => x.MaterialCategoryId,
                        principalTable: "InGame_MaterialCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InGame_Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Description = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IconUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    ItemCategoryId = table.Column<Guid>(type: "char(36)", nullable: false),
                    WikiUri = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Mass = table.Column<float>(type: "float", nullable: true),
                    CorrosionResistance = table.Column<float>(type: "float", nullable: true),
                    PrimaryMaterialId = table.Column<Guid>(type: "char(36)", nullable: true),
                    ElectricInput = table.Column<float>(type: "float", nullable: true),
                    ElectricOutput = table.Column<float>(type: "float", nullable: true),
                    ElectricityConversionBonusFactor = table.Column<float>(type: "float", nullable: true),
                    ElectricCapacity = table.Column<float>(type: "float", nullable: true),
                    PropellantInput = table.Column<float>(type: "float", nullable: true),
                    PropellantOutput = table.Column<float>(type: "float", nullable: true),
                    PropellantConversionBonusFactor = table.Column<float>(type: "float", nullable: true),
                    PropellantCapacity = table.Column<float>(type: "float", nullable: true),
                    ThrustPower = table.Column<float>(type: "float", nullable: true),
                    Tier = table.Column<int>(type: "int", nullable: true),
                    FuelCapacity = table.Column<float>(type: "float", nullable: true),
                    FuelInput = table.Column<float>(type: "float", nullable: true),
                    HeatGeneration = table.Column<float>(type: "float", nullable: true),
                    ElectricityPerShot = table.Column<float>(type: "float", nullable: true),
                    ElectricityPerRecharge = table.Column<float>(type: "float", nullable: true),
                    HeatGenerationPerShot = table.Column<float>(type: "float", nullable: true),
                    HeatDissipation = table.Column<float>(type: "float", nullable: true),
                    MinMuzzleVelocity = table.Column<float>(type: "float", nullable: true),
                    MaxMuzzleVelocity = table.Column<float>(type: "float", nullable: true),
                    RateOfFire = table.Column<float>(type: "float", nullable: true),
                    ChargeCapacity = table.Column<float>(type: "float", nullable: true),
                    MagazineCapacity = table.Column<float>(type: "float", nullable: true),
                    ProjectileMass = table.Column<float>(type: "float", nullable: true),
                    ProjectileEnergy = table.Column<float>(type: "float", nullable: true),
                    ProjectileLifetime = table.Column<float>(type: "float", nullable: true),
                    ResearchPointsCube = table.Column<float>(type: "float", nullable: true),
                    ResearchPointsPower = table.Column<float>(type: "float", nullable: true),
                    ResearchPointsShield = table.Column<float>(type: "float", nullable: true),
                    ResearchPointsGear = table.Column<float>(type: "float", nullable: true),
                    OldId = table.Column<uint>(type: "int unsigned", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGame_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InGame_Items_InGame_ItemCategories_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "InGame_ItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InGame_Items_InGame_Materials_PrimaryMaterialId",
                        column: x => x.PrimaryMaterialId,
                        principalTable: "InGame_Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DiscordId",
                table: "AspNetUsers",
                column: "DiscordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_RefreshTokens_UserId",
                table: "Authentication_RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InGame_ItemCategories_ParentId",
                table: "InGame_ItemCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_InGame_Items_ItemCategoryId",
                table: "InGame_Items",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InGame_Items_PrimaryMaterialId",
                table: "InGame_Items",
                column: "PrimaryMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_InGame_Materials_MaterialCategoryId",
                table: "InGame_Materials",
                column: "MaterialCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Authentication_RefreshTokens");

            migrationBuilder.DropTable(
                name: "InGame_Items");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "InGame_ItemCategories");

            migrationBuilder.DropTable(
                name: "InGame_Materials");

            migrationBuilder.DropTable(
                name: "InGame_MaterialCategories");
        }
    }
}
