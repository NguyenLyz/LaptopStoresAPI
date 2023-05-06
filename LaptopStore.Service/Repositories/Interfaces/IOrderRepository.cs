using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface IOrderRepository : IStringF1GenericRepository<Order>
    {
        List<Order> GetByUserId(string _userId);
        List<OrderRequestModel> GetAll();
        IQueryable<Order> GetSuccessByYear(int year);
        IQueryable<ChartResponseModel> GetBrandChart(int month, int year);
        IQueryable<ChartResponseModel> GetCategoryChart(int month, int year);
        IQueryable<ChartResponseModel> GetSeriesChart(int month, int year);
    }
}
