﻿using LaptopStore.Data.Context;
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
        public IQueryable<OrderDetail> GetByOrderId(string id)
        {
            var query = from orderDetail in _context.OrderDetails
                        join product in _context.Products on orderDetail.ProductId equals product.Id
                        where orderDetail.OrderId == id
                        select new OrderDetail
                        {
                            OrderId = orderDetail.OrderId,
                            ProductId = orderDetail.ProductId,
                            Amount = orderDetail.Amount,
                            Quantity = orderDetail.Quantity,
                            Product = product
                        };
            return query;
        }
        public IQueryable<OrderDetail> GetByMonthAndYear(int month, int year)
        {
            var query = from order in _context.Orders
                        join orderDetail in _context.OrderDetails on order.Id equals orderDetail.OrderId
                        where order.OrderDate.Month == month && order.OrderDate.Year == year && order.Status == 3
                        select new OrderDetail
                        {
                            OrderId = orderDetail.OrderId,
                            ProductId = orderDetail.ProductId,
                            Amount = orderDetail.Amount,
                            Quantity = orderDetail.Quantity,
                        };
            return query;
        }
    }
}
