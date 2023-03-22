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
    public class OrderDetailRepository : F0GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public IQueryable<OrderDetail> GetByOrderId(int id)
        {
            return _context.OrderDetails.Where(x => x.OrderId == id);
        }
    }
}
