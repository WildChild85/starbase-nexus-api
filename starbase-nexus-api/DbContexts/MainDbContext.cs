using starbase_nexus_api.Entities;
using starbase_nexus_api.Entities.Authentication;
using starbase_nexus_api.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using starbase_nexus_api.Entities.InGame;
using starbase_nexus_api.Entities.Yolol;
using starbase_nexus_api.Entities.Social;
using starbase_nexus_api.Entities.Knowledge;

namespace starbase_nexus_api.DbContexts
{
    public class MainDbContext : IdentityDbContext<User, Role, string>
    {
        public MainDbContext(DbContextOptions<MainDbContext> options): base(options)
        {

        }

        public DbSet<RefreshToken> Authentication_RefreshTokens { get; set; }

        public DbSet<MaterialCategory> InGame_MaterialCategories { get; set; }
        public DbSet<Material> InGame_Materials { get; set; }
        public DbSet<ItemCategory> InGame_ItemCategories { get; set; }
        public DbSet<Item> InGame_Items { get; set; }
        public DbSet<YololProject> Yolol_YololProjects { get; set; }
        public DbSet<YololScript> Yolol_YololScripts { get; set; }

        public DbSet<Like> Social_Likes { get; set; }
        public DbSet<Company> Social_Companies { get; set; }
        public DbSet<Guide> Knowledge_Guides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Do not allow cascading deletes! This prevents unwanted deletions and loss of data.
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<User>()
                .HasIndex(u => u.DiscordId)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Handle added entries
            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> createdEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added));

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry in createdEntries)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTimeOffset.UtcNow;
            }

            // Handle updated entries
            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> updatedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Modified));

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry in updatedEntries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTimeOffset.UtcNow;
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            // Handle added entries
            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> createdEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added));

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry in createdEntries)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTimeOffset.UtcNow;
            }

            // Handle updated entries
            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> updatedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Modified));

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry in updatedEntries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTimeOffset.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
