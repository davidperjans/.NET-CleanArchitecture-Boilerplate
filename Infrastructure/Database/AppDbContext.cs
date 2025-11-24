using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define DbSets for your entities here
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entity mappings here
            // modelBuilder.Entity<YourEntity>(entity =>
            // {
            //     entity.ToTable("YourTableName");
            //     entity.HasKey(e => e.Id);
            //     // Additional configurations...
            // });
        }
    }
}
