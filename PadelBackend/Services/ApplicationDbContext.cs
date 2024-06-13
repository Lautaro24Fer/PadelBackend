using Microsoft.EntityFrameworkCore;
using PadelBackend.Models.User;

namespace PadelBackend.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();

            modelBuilder.Entity<User>().HasData(
                new User { Email = "Lautaro@gmail.com", Id = 1, Name = "Lautaro Fernandez", Password = "12345678", UserName = "lauta_ro"}
                );
        }
    }
}
