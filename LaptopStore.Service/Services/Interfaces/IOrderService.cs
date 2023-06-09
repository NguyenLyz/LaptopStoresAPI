﻿using LaptopStore.Service.RequestModels;
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
        Task<string> Add(OrderRequestModel request, string _userId);
        Task<OrderResponseModel> GetById(string id);
        List<OrderRequestModel> GetByUserId(string userId);
        List<OrderRequestModel> GetByShipperId(string shipperId);
        Task CancelOrderUser(string id, string _userId);
        Task CancelOrder(string id);
        Task ProcessOrder(string id);
        List<OrderRequestModel> GetAll();
        List<ChartResponseModel> GetIncomeChart(int year);
        List<ChartResponseModel> GetSoldChart(int year);
        //List<ChartResponseModel> GetBrandCircleChart(int year);
        List<ChartResponseModel> GetBrandCircleChart(int month, int year);
        List<ChartResponseModel> GetCategoryCircleChart(int month, int year);
        List<ChartResponseModel> GetSeriesCircleChart(int month, int year);
    }
}
