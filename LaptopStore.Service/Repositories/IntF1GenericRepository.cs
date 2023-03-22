using LaptopStore.Data.Context;
using LaptopStore.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories
{
    public class IntF1GenericRepository<T> : F0GenericRepository<T>, IIntF1GenericRepository<T> where T : class
    {
        public IntF1GenericRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
