using Limedika.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Limedika.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<Location>()
                .HasIndex(m => m.Address)
                .IsUnique();
        }
    }
}