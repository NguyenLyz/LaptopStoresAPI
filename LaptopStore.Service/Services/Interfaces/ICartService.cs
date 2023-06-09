﻿using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartRequestModel> Add(CartRequestModel request, string _userId);
        //List<CartRequestModel> GetAll();
        Task Delete(int _productId, string _userId);
        Task<CartRequestModel> Update(CartRequestModel request, string _userId);
        Task<List<CartResponseModel>> GetByUserId(string _userId);
    }
}
