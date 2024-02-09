using Microsoft.EntityFrameworkCore;
using MultiTenant_MVC.Models.Entities;

namespace MultiTenant_MVC.DBContext
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        { }

        public DbSet<MasterUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasterUser>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<MasterUser>().HasData(
                new MasterUser
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "test@libero.it",
                    Password = "1234",
                    ConnectionString = "",
                    IsActive = true,
                    IsAdmin = true
                }
            );
        }
    }
}
