using LaptopStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface IShipperOrderRepository : IF0GenericRepository<ShipperOrder>
    {
        ShipperOrder GetByOrderId(string orderId);
    }
}
