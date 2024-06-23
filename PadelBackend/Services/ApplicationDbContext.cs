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
            // Racketas
            modelBuilder.Entity<Racket>();
            modelBuilder.Entity<Racket>().HasData(
                new Racket { Id = 1, Name = "Racketa 1", Category = "Diamante", Description = "esta es una descripcion de prueba", Image = "https://imgs.search.brave.com/f-4T_e5tykKzUymoLjwVP7dzRg6pXn08rTfXQckBI-g/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9kMjho/aTkzZ3I2OTdvbC5j/bG91ZGZyb250Lm5l/dC81YTFhNzBlMS02/MzIxLTY5NDQtZWQ5/My02N2E0ODU1MDM1/MDQvaW1nL1Byb2R1/Y3RvLzk3YmZmNWQ3/LWU5NGItYzY4MS1i/NGNjLTI4MDgzN2Ux/NWY2YS9BQUExLTEz/LTY1OWVhZDdkNzRh/YTAuanBn", Price = 12.19F},
                new Racket { Id = 2, Name = "Racketa 2", Category = "Redonda", Description = "esta es una descripcion de prueba 2", Image = "https://imgs.search.brave.com/f-4T_e5tykKzUymoLjwVP7dzRg6pXn08rTfXQckBI-g/rs:fit:860:0:0/g:ce/aHR0cHM6Ly9kMjho/aTkzZ3I2OTdvbC5j/bG91ZGZyb250Lm5l/dC81YTFhNzBlMS02/MzIxLTY5NDQtZWQ5/My02N2E0ODU1MDM1/MDQvaW1nL1Byb2R1/Y3RvLzk3YmZmNWQ3/LWU5NGItYzY4MS1i/NGNjLTI4MDgzN2Ux/NWY2YS9BQUExLTEz/LTY1OWVhZDdkNzRh/YTAuanBn", Price = 1922.19F}
                );
        }
    }
}
