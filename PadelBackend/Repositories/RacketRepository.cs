using Microsoft.EntityFrameworkCore;
using PadelBackend.Models.Query.Dto;
using PadelBackend.Models.Racket;
using PadelBackend.Models.Racket.Dto;
using PadelBackend.Services;
using System.Linq.Expressions;
using System.Web.Http.Filters;

namespace PadelBackend.Repositories
{

    public interface IRacketRepository : IRepository<Racket> 
    {
        public Task<List<Racket>> Get(QueryDto filters);
    }
    public class RacketRepository : Repository<Racket>, IRacketRepository
    {
        public RacketRepository(ApplicationDbContext db) : base(db) { }

        public async Task<List<Racket>> Get(QueryDto filters)
        {
            IQueryable<Racket> query = dbSet;
           
            if (!string.IsNullOrEmpty(filters.Brand))
            {
                query = query.Where(p => p.Brand == filters.Brand);
                Console.WriteLine("Filtrando por brand");
            }
            if (!string.IsNullOrEmpty(filters.Model))
            {
                query = query.Where(p => p.Model == filters.Model);
                Console.WriteLine("Filtrando por modelo");
            }
            if (filters.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filters.MinPrice.Value);
                Console.WriteLine("Filtrando por precio minimo");
            }
            if (filters.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filters.MaxPrice.Value);
                Console.WriteLine("Filtrando por precio maximo");
            }
            query = query.OrderBy(p => p.Id);
            if (filters.Limit.HasValue && filters.Limit.Value > 0)
            {
                query = query.Take(filters.Limit.Value);
                Console.WriteLine("Filtrando por limite de registros");
            }
            var rackets = await query.ToListAsync();
            return rackets;
        }
    }
}
