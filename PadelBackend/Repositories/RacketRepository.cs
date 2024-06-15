using PadelBackend.Models.Rackets;
using PadelBackend.Services;

namespace PadelBackend.Repositories
{
    public class RacketRepository : Repository<Racket>
    {
        public RacketRepository(ApplicationDbContext context) : base(context) { }

        // Aquí puedes agregar métodos específicos para Racket si es necesario
    }
}
