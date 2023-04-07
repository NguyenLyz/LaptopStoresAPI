using AutoMapper;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using LaptopStore.Service.ResponseModels;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderRequestModel> Add(OrderRequestModel request, string _userId)
        {
            try
            {
                var order = new Order
                {
                    UserId = new Guid(_userId),
                    Status = 0,
                    OrderDate = DateTime.Now,
                    ShipName = request.ShipName,
                    ShipAddress = request.ShipAddress,
                    ShipPhone = request.ShipPhone,
                    Note = request.Note,
                    ShipMethod = request.ShipMethod,
                };
                if(order.ShipMethod == 1)
                {
                    order.OrderValue = 100000;
                }
                order = await _unitOfWork.OrderRepository.AddAsync(order);
                await _unitOfWork.SaveAsync();
                List<CartResponseModel> carts = _unitOfWork.CartRepository.GetByUserId(_userId);
                foreach (var cart in carts)
                {
                    var product = _unitOfWork.ProductRepository.GetById(cart.ProductId);
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = cart.ProductId,
                        Amount = (product.Price - ((product.Discount / 100) * product.Price)) * cart.Quantity,
                        Quantity = cart.Quantity
                    };
                    order.OrderValue = order.OrderValue + orderDetail.Amount;
                    await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
                    await _unitOfWork.SaveAsync();
                }
                _unitOfWork.CartRepository.DeleteByUserId(_userId);
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.SaveAsync();
                var result = new OrderRequestModel
                {
                    Id = order.Id,
                    OrderValue = order.OrderValue,
                    Status = order.Status,
                    ShipName = order.ShipName,
                    ShipAddress = order.ShipAddress,
                    ShipPhone = order.ShipPhone,
                    Note = order.Note,
                    ShipMethod = order.ShipMethod,
                };
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task CancelOrderUser(int id, string _userId)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);
                if (order.UserId == new Guid(_userId))
                {
                    if(order.Status == 0)
                    {
                        order.Status = 5;
                        _unitOfWork.OrderRepository.Update(order);
                        await _unitOfWork.SaveAsync();
                        return;
                    }
                    if(order.Status >= 1 && order.Status <= 2)
                    {
                        order.Status = 4;
                        _unitOfWork.OrderRepository.Update(order);
                        await _unitOfWork.SaveAsync();
                        return;
                    }
                }
                throw new Exception("Fail to Cancel Order");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task CancelOrder(int id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);
                if(order.Status == 0)
                {
                    order.Status = 5;
                    _unitOfWork.OrderRepository.Update(order);
                    return;
                }
                if(order.Status >= 1 && order.Status <= 2)
                {
                    order.Status = 5;
                    await _unitOfWork.ProductRepository.CancelProcessing(id);
                    _unitOfWork.OrderRepository.Update(order);
                    return;
                }
                throw new Exception("Fail to cancel order");
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task ProcessOrder(int id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);
                if (order.Status < 3)
                {
                    order.Status++;
                    if(order.Status == 1)
                    {
                        await _unitOfWork.ProductRepository.SuccessfulProcessing(id);
                    }
                    _unitOfWork.OrderRepository.Update(order);
                    await _unitOfWork.SaveAsync();
                    return;
                }
                if(order.Status == 4)
                {
                    order.Status++;
                    await _unitOfWork.ProductRepository.CancelProcessing(id);
                    _unitOfWork.OrderRepository.Update(order);
                    await _unitOfWork.SaveAsync();
                    return;
                }
                throw new Exception("Fail to process order");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<OrderRequestModel> GetById(int id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);
                order.OrderDetails = await _unitOfWork.OrderDetailRepository.GetByOrderId(order.Id).ToListAsync();
                return _mapper.Map<Order, OrderRequestModel>(order);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<OrderRequestModel> GetByUserId(string userId)
        {
            try
            {
                return _mapper.Map<List<Order>, List<OrderRequestModel>>(_unitOfWork.OrderRepository.GetByUserId(userId));
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<OrderRequestModel> GetAll()
        {
            try
            {
                return _mapper.Map<List<Order>, List<OrderRequestModel>>(_unitOfWork.OrderRepository.GetAll().OrderBy(x => x.Status).ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<ChartResponseModel> GetIncomeChart(int year)
        {
            try
            {
                if(year == 0)
                {
                    year = DateTime.Now.Year;
                }
                var order = _unitOfWork.OrderRepository.GetSuccessByYear(year);
                var result = new List<ChartResponseModel>();
                for(int m = 1; m <= 12; m++)
                {
                    var data = order.Where(x => x.OrderDate.Month == m).Sum(x => x.OrderValue);
                    var month = new ChartResponseModel
                    {
                        Key = m.ToString(),
                        Value = data,
                    };
                    result.Add(month);
                }
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<ChartResponseModel> GetSoldChart(int year)
        {
            try
            {
                if(year == 0)
                {
                    year = DateTime.Now.Year;
                }
                var result = new List<ChartResponseModel>();
                for(int m = 1; m <= 12; m++)
                {
                    var orderDetail = _unitOfWork.OrderDetailRepository.GetByMonthAndYear(m, year);
                    var sum = orderDetail.Sum(x => x.Quantity);
                    var month = new ChartResponseModel
                    {
                        Key = m.ToString(),
                        Value = sum,
                    };
                    result.Add(month);
                }
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        /*public List<ChartResponseModel> GetBrandCircleChart(int year)
        {
            try
            {
                var result = new List<ChartResponseModel>();
                var order = _unitOfWork.OrderRepository.GetBrandChart(4, year);
                foreach(var brand in order)
                {
                    var sum = order.Where(x => x.Name == brand.Name).Sum(x => x.Id);
                    var data = new ChartResponseModel
                    {
                        Key = brand.Name,
                        Value = sum,
                    };
                    result.Add(data);
                }
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }*/
        public List<ChartResponseModel> GetBrandCircleChart(int month, int year)
        {
            try
            {
                if(month == 0 || year == 0)
                {
                    month = DateTime.Now.Month;
                    year = DateTime.Now.Year;
                }
                var brands = _unitOfWork.OrderRepository.GetBrandChart(month, year).ToList();
                var result = new List<ChartResponseModel>();
                var loops = brands.GroupBy(x => x.Key).ToList();
                foreach(var loop in loops)
                {
                    var sum = brands.Where(x => x.Key == loop.Key).Sum(x => x.Value);
                    var data = new ChartResponseModel
                    {
                        Key = loop.Key,
                        Value = sum
                    };
                    result.Add(data);
                }
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<ChartResponseModel> GetCategoryCircleChart(int month, int year)
        {
            try
            {
                if(month == 0 || year == 0)
                {
                    month = DateTime.Now.Month;
                    year = DateTime.Now.Year;
                }
                var categories = _unitOfWork.OrderRepository.GetCategoryChart(month, year).ToList();
                var result = new List<ChartResponseModel>();
                var loops = categories.GroupBy(x => x.Key).ToList();
                foreach(var loop in loops)
                {
                    var sum = categories.Where(x => x.Key == loop.Key).Sum(x => x.Value);
                    var data = new ChartResponseModel
                    {
                        Key = loop.Key,
                        Value = sum
                    };
                    result.Add(data);
                }
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<ChartResponseModel> GetSeriesCircleChart(int month, int year)
        {
            try
            {
                if (month == 0 || year == 0)
                {
                    month = DateTime.Now.Month;
                    year = DateTime.Now.Year;
                }
                var series = _unitOfWork.OrderRepository.GetSeriesChart(month, year).ToList();
                var result = new List<ChartResponseModel>();
                var loops = series.GroupBy(x => x.Key).ToList();
                foreach (var loop in loops)
                {
                    var sum = series.Where(x => x.Key == loop.Key).Sum(x => x.Value);
                    var data = new ChartResponseModel
                    {
                        Key = loop.Key,
                        Value = sum
                    };
                    result.Add(data);
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
