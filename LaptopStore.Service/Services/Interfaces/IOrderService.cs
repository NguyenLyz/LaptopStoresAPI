using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderRequestModel> Add(OrderRequestModel request, string _userId);
        Task<OrderRequestModel> GetById(int id);
        List<OrderRequestModel> GetByUserId(string userId);
        Task UpdateOrderStatus(int id, int status);
        List<OrderRequestModel> GetAll();
    }
}
