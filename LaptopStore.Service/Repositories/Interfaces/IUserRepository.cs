using LaptopStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface IUserRepository : IF0GenericRepository<User>
    {
        User GetByPhone(string phone);
        User GetByEmail(string email);
        User GetById(string id);
    }
}
