using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface IStringF1GenericRepository<T> where T : class
    {
        T GetById(string id);
    }
}
