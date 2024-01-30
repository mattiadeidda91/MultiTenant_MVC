using Microsoft.EntityFrameworkCore;
using MultiTenant_MVC.Models.Entities;

namespace MultiTenant_MVC.DBContext
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(string connectionString)
            : base(new DbContextOptionsBuilder<TenantDbContext>().UseSqlServer(connectionString).Options)
        {
        }
        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options) { }

        public DbSet<Orders> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura il mapping dell'entità Orders se necessario
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Type).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasMaxLength(10);
            });
        }
    }
}
