using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Interfaces.RepositoryInterfaces;
using System;
using System.Linq.Expressions;

namespace OrderManagementSystem.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly OMSDbContext _context;

        protected readonly DbSet<T> _dbSet;

        public BaseRepository(OMSDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
           return await _dbSet
          .Where(predicate)
          .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        } 

    }
}
