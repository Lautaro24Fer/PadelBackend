﻿using Microsoft.EntityFrameworkCore;
using PadelBackend.Services;
using System.Linq.Expressions;

namespace PadelBackend.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<List<T>> Get(Expression<Func<T, bool>>? filter = null);
        public Task<T> GetOne(Expression<Func<T, bool>>? filter = null);
        public Task CreateOne(T entity);
        public Task Save();
        public Task<T> Update(T entity);
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
            if(filter != null)
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

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> Update(T entity)
        {
            dbSet.Update(entity);
            await Save();
            return entity;
        }
    }
}
