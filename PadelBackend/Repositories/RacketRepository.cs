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
            }
            if (!string.IsNullOrEmpty(filters.Model))
            {
                query = query.Where(p => p.Model == filters.Model);
            }
            if (filters.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filters.MinPrice.Value);
            }
            if (filters.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filters.MaxPrice.Value);
            }
            if (filters.Limit.HasValue)
            {
                query = query.OrderBy(p => p.Id).Take(filters.Limit.Value);
            }
            var rackets = await query.ToListAsync();
            return rackets;
        }
    }
}
