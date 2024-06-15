using Microsoft.EntityFrameworkCore;
using PadelBackend.Models.Rackets;
using PadelBackend.Models.User;

namespace PadelBackend.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Racket> Rackets { get; set; }
        public DbSet<User> Users { get; set; } // Para cuando haya usuarios

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            // Configuración adicional

        }
    }
}
