using PadelBackend.Models.User;
using PadelBackend.Services;

namespace PadelBackend.Repositories
{
    public interface IUserRepository : IRepository<User> { }
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db) : base(db) { }
    }
}
