using LaptopStore.Data.Context;
using LaptopStore.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories
{
    public class F0GenericRepository<T> : IF0GenericRepository<T> where T : class
    {
        protected readonly LaptopStoreDbContext _context;
        public F0GenericRepository(LaptopStoreDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }
        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }
        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            await _context.Set<List<T>>().AddRangeAsync(entities);
            return entities;
        }
        public List<T> UpdateRange(List<T> entities)
        {
            _context.Set<List<T>>().UpdateRange(entities);
            return entities;
        }
    }
}
