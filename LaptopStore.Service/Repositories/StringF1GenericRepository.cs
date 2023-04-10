
using LaptopStore.Data.Context;
using LaptopStore.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories
{
    public class StringF1GenericRepository<T> : F0GenericRepository<T>, IStringF1GenericRepository<T> where T : class
    {
        public StringF1GenericRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public T GetById(string id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
