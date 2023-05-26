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
    public class ShipperOrderRepository : F0GenericRepository<ShipperOrder>, IShipperOrderRepository
    {
        public ShipperOrderRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public ShipperOrder GetByOrderId(string orderId)
        {
            return _context.ShipperOrders.Where(x => x.OrderId == orderId).FirstOrDefault();
        }
    }
}
