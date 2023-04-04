using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponseModels;
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
        Task CancelOrder(int id);
        Task ProcessOrder(int id);
        List<OrderRequestModel> GetAll();
        List<ChartResponseModel> GetIncomeChart(int year);
        List<ChartResponseModel> GetSoldChart(int year);
        List<ChartResponseModel> GetBrandCircleChart(int year);
        List<ChartResponseModel> GetBrandChartFromOrderInfo(int year, int month);
    }
}
