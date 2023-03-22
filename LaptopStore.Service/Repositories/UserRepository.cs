using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories
{
    public class UserRepository : F0GenericRepository<User>, IUserRepository
    {
        public UserRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public User GetByPhone(string phone)
        {
            var user = _context.Users.Where(x => x.Phone == phone).FirstOrDefault();
            return user;
        }
        public User GetByEmail(string email)
        {
            var user = _context.Users.Where(x => x.Email == email).FirstOrDefault();
            return user;
        }
        public User GetById(string id)
        {
            var user = _context.Users.Where(x => x.Id == new Guid(id)).FirstOrDefault();
            return user;
        }
    }
}
