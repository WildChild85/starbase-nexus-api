﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using starbase_nexus_api.DbContexts;

namespace starbase_nexus_api.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Authentication.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("ExpiresAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("IpAddress")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Authentication_RefreshTokens");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Identity.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Identity.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AboutMe")
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AvatarUri")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DiscordId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LastLogin")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("DiscordId")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.InGame.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<float?>("ChargeCapacity")
                        .HasColumnType("float");

                    b.Property<float?>("CorrosionResistance")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<float?>("ElectricCapacity")
                        .HasColumnType("float");

                    b.Property<float?>("ElectricInput")
                        .HasColumnType("float");

                    b.Property<float?>("ElectricOutput")
                        .HasColumnType("float");

                    b.Property<float?>("ElectricityConversionBonusFactor")
                        .HasColumnType("float");

                    b.Property<float?>("ElectricityPerRecharge")
                        .HasColumnType("float");

                    b.Property<float?>("ElectricityPerShot")
                        .HasColumnType("float");

                    b.Property<float?>("FuelCapacity")
                        .HasColumnType("float");

                    b.Property<float?>("FuelInput")
                        .HasColumnType("float");

                    b.Property<float?>("HeatDissipation")
                        .HasColumnType("float");

                    b.Property<float?>("HeatGeneration")
                        .HasColumnType("float");

                    b.Property<float?>("HeatGenerationPerShot")
                        .HasColumnType("float");

                    b.Property<string>("IconUri")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("ItemCategoryId")
                        .HasColumnType("char(36)");

                    b.Property<float?>("MagazineCapacity")
                        .HasColumnType("float");

                    b.Property<float?>("Mass")
                        .HasColumnType("float");

                    b.Property<float?>("MaxMuzzleVelocity")
                        .HasColumnType("float");

                    b.Property<float?>("MinMuzzleVelocity")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<uint?>("OldId")
                        .HasColumnType("int unsigned");

                    b.Property<Guid?>("PrimaryMaterialId")
                        .HasColumnType("char(36)");

                    b.Property<float?>("ProjectileEnergy")
                        .HasColumnType("float");

                    b.Property<float?>("ProjectileLifetime")
                        .HasColumnType("float");

                    b.Property<float?>("ProjectileMass")
                        .HasColumnType("float");

                    b.Property<float?>("PropellantCapacity")
                        .HasColumnType("float");

                    b.Property<float?>("PropellantConversionBonusFactor")
                        .HasColumnType("float");

                    b.Property<float?>("PropellantInput")
                        .HasColumnType("float");

                    b.Property<float?>("PropellantOutput")
                        .HasColumnType("float");

                    b.Property<float?>("RateOfFire")
                        .HasColumnType("float");

                    b.Property<float?>("ResearchPointsCube")
                        .HasColumnType("float");

                    b.Property<float?>("ResearchPointsGear")
                        .HasColumnType("float");

                    b.Property<float?>("ResearchPointsPower")
                        .HasColumnType("float");

                    b.Property<float?>("ResearchPointsShield")
                        .HasColumnType("float");

                    b.Property<float?>("ThrustPower")
                        .HasColumnType("float");

                    b.Property<int?>("Tier")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<float?>("WarmupTime")
                        .HasColumnType("float");

                    b.Property<string>("WikiUri")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ItemCategoryId");

                    b.HasIndex("PrimaryMaterialId");

                    b.ToTable("InGame_Items");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.InGame.ItemCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<uint?>("OldId")
                        .HasColumnType("int unsigned");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("InGame_ItemCategories");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.InGame.Material", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double?>("Armor")
                        .HasColumnType("double");

                    b.Property<double?>("CorrosionResistance")
                        .HasColumnType("double");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("Density")
                        .HasColumnType("double");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("IconUriOreChunk")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("IconUriRaw")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("IconUriRefined")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("MaterialCategoryId")
                        .HasColumnType("char(36)");

                    b.Property<double?>("MinArmor")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<uint?>("OldId")
                        .HasColumnType("int unsigned");

                    b.Property<double?>("OreDensity")
                        .HasColumnType("double");

                    b.Property<double?>("StructuralDurability")
                        .HasColumnType("double");

                    b.Property<double?>("Transformability")
                        .HasColumnType("double");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("VoxelPenetrationMultiplier")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("MaterialCategoryId");

                    b.ToTable("InGame_Materials");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.InGame.MaterialCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<uint?>("OldId")
                        .HasColumnType("int unsigned");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("InGame_MaterialCategories");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Knowledge.Guide", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Bodytext")
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<uint?>("OldId")
                        .HasColumnType("int unsigned");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("YoutubeVideoUri")
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Knowledge_Guides");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Social.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("AboutUs")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("DiscordUri")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LogoUri")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<uint?>("OldId")
                        .HasColumnType("int unsigned");

                    b.Property<string>("TwitchUri")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("WebsiteUri")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("YoutubeUri")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Social_Companies");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Social.Like", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<uint?>("OldId")
                        .HasColumnType("int unsigned");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<Guid?>("YololProjectId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("YololProjectId");

                    b.ToTable("Social_Likes");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Yolol.YololProject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Documentation")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<uint?>("OldId")
                        .HasColumnType("int unsigned");

                    b.Property<string>("PreviewImageUri")
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("YoutubeVideoUri")
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Yolol_YololProjects");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Yolol.YololScript", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<uint?>("OldId")
                        .HasColumnType("int unsigned");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Yolol_YololScripts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("starbase_nexus_api.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Authentication.RefreshToken", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.InGame.Item", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.InGame.ItemCategory", "ItemCategory")
                        .WithMany()
                        .HasForeignKey("ItemCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("starbase_nexus_api.Entities.InGame.Material", "PrimaryMaterial")
                        .WithMany()
                        .HasForeignKey("PrimaryMaterialId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ItemCategory");

                    b.Navigation("PrimaryMaterial");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.InGame.ItemCategory", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.InGame.ItemCategory", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.InGame.Material", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.InGame.MaterialCategory", "MaterialCategory")
                        .WithMany()
                        .HasForeignKey("MaterialCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MaterialCategory");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Knowledge.Guide", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Identity.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Social.Company", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Identity.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Social.Like", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Identity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("starbase_nexus_api.Entities.Yolol.YololProject", "YololProject")
                        .WithMany()
                        .HasForeignKey("YololProjectId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");

                    b.Navigation("YololProject");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Yolol.YololProject", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Identity.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("starbase_nexus_api.Entities.Yolol.YololScript", b =>
                {
                    b.HasOne("starbase_nexus_api.Entities.Yolol.YololProject", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Project");
                });
#pragma warning restore 612, 618
        }
    }
}
