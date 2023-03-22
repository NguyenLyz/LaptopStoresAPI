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
    public class ProductRepository : IntF1GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public async Task SuccessfulProcessing(int orderId)
        {
            var query = from orderDetail in _context.OrderDetails
                        where orderDetail.OrderId == orderId
                        select orderDetail;
            var query2 = from product in _context.Products
                         select product;
            var result = query.ToList();
            var result2 = query2.ToList();
            for (int i = 0; i < query.Count(); i++)
            {
                for (int j = 0; j < query2.Count(); j++)
                {
                    if (result[i].ProductId == result2[j].Id)
                    {
                        result2[j].Sold = result2[j].Sold + result[i].Quantity;
                    }
                }
            }
            _context.Products.UpdateRange(result2);
            await _context.SaveChangesAsync();
        }
    }
}
