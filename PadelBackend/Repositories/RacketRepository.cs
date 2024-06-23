using PadelBackend.Models.Racket;
using PadelBackend.Services;

namespace PadelBackend.Repositories
{

    public interface IRacketRepository : IRepository<Racket> { }
    public class RacketRepository : Repository<Racket>, IRacketRepository
    {
        public RacketRepository(ApplicationDbContext db) : base(db) { }
    }
}
