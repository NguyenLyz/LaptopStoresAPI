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
    public class OrderRepository : IntF1GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public List<Order> GetByUserId(string _userId)
        {
            return _context.Orders.Where(x => x.UserId == new Guid(_userId)).ToList();
        }
    }
}
