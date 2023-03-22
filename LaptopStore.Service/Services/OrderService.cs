using AutoMapper;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
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
                };
                order = await _unitOfWork.OrderRepository.AddAsync(order);
                await _unitOfWork.SaveAsync();
                /*List<CartResponeModel> carts = _unitOfWork.CartRepository.GetByUserId(_userId);
                foreach (var cart in carts)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = cart.ProductId,
                        Amount = (cart.Price - ((cart.Discount / 100) * cart.Price)) * cart.Quantity,
                        Quantity = cart.Quantity
                    };
                    order.OrderValue = order.OrderValue + orderDetail.Amount;
                    await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
                    await _unitOfWork.SaveAsync();
                }*/
                _unitOfWork.CartRepository.DeleteByUserId(_userId);
                /*_unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.SaveAsync();*/
                var result = new OrderRequestModel
                {
                    Id = order.Id,
                    OrderValue = order.OrderValue,
                    Status = order.Status,
                    ShipName = order.ShipName,
                    ShipAddress = order.ShipAddress,
                    ShipPhone = order.ShipPhone,
                    Note = order.Note
                };
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task UpdateOrderStatus(int id, int status)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);
                if(status != 3)
                {
                    if(order.Status < status)
                    {
                        order.Status = status;
                        if(order.Status == 1)
                        {
                            await _unitOfWork.ProductRepository.SuccessfulProcessing(id);
                        }
                    }
                }
                else
                {
                    if(order.Status == 0)
                    {
                        order.Status = status;
                    }
                }
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.SaveAsync();
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

    }
}
