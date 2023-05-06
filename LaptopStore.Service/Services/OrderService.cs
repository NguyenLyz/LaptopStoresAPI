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
        public async Task<string> Add(OrderRequestModel request, string _userId)
        {
            try
            {
                var Time = DateTime.Now;
                var idMiddle = Guid.NewGuid();
                var order = new Order
                {
                    Id = Time.ToString("hhdd" + idMiddle + "MMyyyyss"),
                    UserId = new Guid(_userId),
                    Status = 0,
                    OrderDate = Time,
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
                var trans = new Transaction
                {
                    OrderId = order.Id,
                    Status = 1,
                    IsPay = false,
                    Amount = order.OrderValue,
                    Message = "Chưa thanh toán"
                };
                await _unitOfWork.TransactionRepository.AddAsync(trans);
                await _unitOfWork.SaveAsync();
                var result = order.Id;
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task CancelOrderUser(string id, string _userId)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);
                var trans = _unitOfWork.TransactionRepository.GetById(order.Id);
                if (order.UserId == new Guid(_userId) && trans.IsPay == false)
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
        public async Task CancelOrder(string id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);
                if(order.Status == 0)
                {
                    order.Status = 5;
                    _unitOfWork.OrderRepository.Update(order);
                    await _unitOfWork.SaveAsync();
                    return;
                }
                if(order.Status >= 1 && order.Status <= 2)
                {
                    order.Status = 5;
                    await _unitOfWork.ProductRepository.CancelProcessing(id);
                    _unitOfWork.OrderRepository.Update(order);
                    await _unitOfWork.SaveAsync();
                    return;
                }
                throw new Exception("Fail to cancel order");
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task ProcessOrder(string id)
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
                    if(order.Status == 3)
                    {
                        var trans = _unitOfWork.TransactionRepository.GetById(order.Id);
                        if(trans.Status == 1)
                        {
                            trans.IsPay = true;
                            trans.Message = "Thành Công.";
                            _unitOfWork.OrderRepository.Update(order);
                            _unitOfWork.TransactionRepository.Update(trans);
                        }
                    }
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
        public async Task<OrderRequestModel> GetById(string id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);
                order.OrderDetails = await _unitOfWork.OrderDetailRepository.GetByOrderId(order.Id).ToListAsync();
                var result = _mapper.Map<Order, OrderRequestModel>(order);
                result.Orderer = _unitOfWork.UserRepository.GetById(order.UserId.ToString()).Name;
                return result;
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
                /*return _mapper.Map<List<Order>, List<OrderRequestModel>>(_unitOfWork.OrderRepository.GetAll().OrderBy(x => x.Status).ToList());*/
                return _unitOfWork.OrderRepository.GetAll();
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
        public List<ChartResponseModel> GetBrandCircleChart(int month, int year)
        {
            try
            {
                if (month == 0)
                {
                    month = DateTime.Now.Month;
                }
                if (year == 0)
                {
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
                if(month == 0)
                {
                    month = DateTime.Now.Month;
                }
                if (year == 0)
                {
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
                if (month == 0)
                {
                    month = DateTime.Now.Month;
                }
                if (year == 0)
                {
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
