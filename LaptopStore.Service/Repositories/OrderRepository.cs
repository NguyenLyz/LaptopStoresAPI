using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.Repositories.Interfaces;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories
{
    public class OrderRepository : StringF1GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public List<OrderRequestModel> GetByUserId(string _userId)
        {
            var query = from order in _context.Orders
                        join trans in _context.Transactions on order.Id equals trans.OrderId
                        where order.UserId == new Guid(_userId)
                        select new OrderRequestModel
                        {
                            Id = order.Id,
                            OrderDate = order.OrderDate,
                            OrderValue = order.OrderValue,
                            Status = order.Status,
                            IsPay = trans.IsPay,
                        };
            return query.ToList();
        }
        public List<OrderRequestModel> GetbyShipperId(string _shipperId)
        {
            var query = from shipperOrder in _context.ShipperOrders
                        join order in _context.Orders on shipperOrder.OrderId equals order.Id
                        join trans in _context.Transactions on order.Id equals trans.OrderId
                        where shipperOrder.UserId == new Guid(_shipperId)
                        select new OrderRequestModel
                        {
                            Id = order.Id,
                            OrderDate = order.OrderDate,
                            OrderValue = order.OrderValue,
                            Status = order.Status,
                            IsPay = trans.IsPay,
                        };
            return query.ToList();
        }
        public List<OrderRequestModel> GetAll()
        {
            var query = from order in _context.Orders
                        join user in _context.Users on order.UserId equals user.Id
                        join trans in _context.Transactions on order.Id equals trans.OrderId
                        select new OrderRequestModel
                        {
                            Id = order.Id,
                            OrderDate = order.OrderDate,
                            OrderValue = order.OrderValue,
                            Orderer = user.Name,
                            Status = order.Status,
                            ShipName = order.ShipName,
                            ShipPhone = order.ShipPhone,
                            ShipAddress = order.ShipAddress,
                            Note = order.Note,
                            ShipMethod = order.ShipMethod,
                            IsPay = trans.IsPay,
                            OrderDetails = order.OrderDetails
                        };
            return query.OrderBy(x => x.Status).ToList();
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
        public void AssignOrderToShipper(string orderId)
        {
            //var employee = _context.Users.Where(x => x.RoleId == new Guid("a1d06430-35af-433a-aefb-283f559059fb"));

            /*var employee_free = from user in _context.Users
                                join shipperOrder in _context.ShipperOrders on user.Id equals shipperOrder.UserId
                                where user.RoleId == new Guid("a1d06430 - 35af - 433a - aefb - 283f559059fb")
                                && shipperOrder.Id == null
                                select user;
            var result = employee_free.First();
            string employee_free_data = employee_free.First().ToString();*/

            var employees = _context.Users.Where(x => x.RoleId == new Guid("a1d06430-35af-433a-aefb-283f559059fb") 
            &&!_context.ShipperOrders.Any(y => y.UserId == x.Id)).ToArray();

            var shipper = new ShipperOrder();
            if(employees.Count() != 0)
            {
                shipper.OrderId = orderId;
                shipper.UserId = employees.FirstOrDefault().Id;
            }
            else
            {
                var userId = _context.ShipperOrders
                    .Include(x => x.Order)
                    .Where(x => x.Order.Status == 2)
                    .GroupBy(x => x.UserId)
                    .Select(x => new
                    {
                        userId = x.Key,
                        count = x.Count()
                    })
                    .OrderBy(x => x.count)
                    .FirstOrDefault().userId;
                shipper.OrderId = orderId;
                shipper.UserId = userId;
            }/*
            shipper = new ShipperOrder()
            {
                OrderId = orderId,
                UserId = userId,
            };*/
            _context.ShipperOrders.Add(shipper);
        }
    }
}
