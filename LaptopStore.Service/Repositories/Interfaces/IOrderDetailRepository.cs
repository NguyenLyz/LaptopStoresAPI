using LaptopStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface IOrderDetailRepository : IF0GenericRepository<OrderDetail>
    {
        IQueryable<OrderDetail> GetByOrderId(int id);
        IQueryable<OrderDetail> GetByMonthAndYear(int month, int year);
    }
}
