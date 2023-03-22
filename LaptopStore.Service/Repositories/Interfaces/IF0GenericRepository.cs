using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface IF0GenericRepository<T> where T : class
    {
        public Task<T> AddAsync(T entity);
        public T Update(T entity);
        public IQueryable<T> GetAll();
        public void Delete(T entity);
        public Task<List<T>> AddRangeAsync(List<T> entities);
        public List<T> UpdateRange(List<T> entities);
    }
}
