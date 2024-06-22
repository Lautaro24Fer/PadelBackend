using Microsoft.EntityFrameworkCore;
using PadelBackend.Models.Racket;
using PadelBackend.Models.User;

namespace PadelBackend.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "lautaro@gmail.com", Name = "Lautaro Fernandez", Password = "12345678", UserName = "lauta"},
                new User { Id = 2, Email = "lautaro1@gmail.com", Name = "Lautaro Fernandez", Password = "12345678", UserName = "lauta2"},
                new User { Id = 3, Email = "lautaro2@gmail.com", Name = "Lautaro Fernandez", Password = "12345678", UserName = "lauta3"}
                );
            // Racketas
            modelBuilder.Entity<Racket>();
            modelBuilder.Entity<Racket>().HasData(
                new Racket { Id = 1, Name = "Racketa 1", Category = RacketCategory.Redonda, Description = "esta es una descripcion de prueba", Image = "esta es una url", Price = 12.19F},
                new Racket { Id = 2, Name = "Racketa 2", Category = RacketCategory.Diamante, Description = "esta es una descripcion de prueba 2", Image = "esta es una url 2", Price = 1922.19F}
                );
        }
    }
}
