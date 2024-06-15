using Microsoft.EntityFrameworkCore;
using PadelBackend.Services;
using System.Linq.Expressions;

namespace PadelBackend.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> Get(Expression<Func<T, bool>>? filter = null);
        Task<T> GetOne(Expression<Func<T, bool>>? filter = null);
        Task CreateOne(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task Save();
    }
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public async Task CreateOne(T entity)
        {
            await dbSet.AddAsync(entity);
            await Save();
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }
        public async Task<T> GetOne(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }
        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
