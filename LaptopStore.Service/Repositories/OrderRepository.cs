using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.Repositories.Interfaces;
using LaptopStore.Service.ResponseModels;
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
        public IQueryable<Order> GetSuccessByYear(int year)
        {
            var orders = _context.Orders.Where(x => x.OrderDate.Year == year && x.Status == 3);
            return orders;
        }
        public IQueryable<ChartResponseModel> GetBrandChart(int month, int year)
        {
            var query = from order in _context.Orders
                        join orderDetail in _context.OrderDetails on order.Id equals orderDetail.OrderId
                        join product in _context.Products on orderDetail.ProductId equals product.Id
                        join brand in _context.Brands on product.BrandId equals brand.Id
                        where order.OrderDate.Year == year && order.OrderDate.Month == month && order.Status == 3
                        select new ChartResponseModel
                        {
                            Key = brand.Name,
                            Value = orderDetail.Quantity,
                        };
            return query;
        }
        public IQueryable<ChartResponseModel> GetCategoryChart(int month, int year)
        {
            var query = from order in _context.Orders
                        join orderDetail in _context.OrderDetails on order.Id equals orderDetail.OrderId
                        join product in _context.Products on orderDetail.ProductId equals product.Id
                        join category in _context.Categories on product.CategoryId equals category.Id
                        where order.OrderDate.Year == year && order.OrderDate.Month == month && order.Status == 3
                        select new ChartResponseModel
                        {
                            Key = category.Name,
                            Value = orderDetail.Quantity
                        };
            return query;
        }
        public IQueryable<ChartResponseModel> GetSeriesChart(int month, int year)
        {
            var query = from order in _context.Orders
                        join orderDetail in _context.OrderDetails on order.Id equals orderDetail.OrderId
                        join product in _context.Products on orderDetail.ProductId equals product.Id
                        join series in _context.Series on product.SeriesId equals series.Id
                        where order.OrderDate.Year == year && order.OrderDate.Month == month && order.Status == 3
                        select new ChartResponseModel
                        {
                            Key = series.Name,
                            Value = orderDetail.Quantity
                        };
            return query;
        }
    }
}
