using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface IIntF1GenericRepository<T> : IF0GenericRepository<T> where T : class
    {
        T GetById(int id);
    }
}
