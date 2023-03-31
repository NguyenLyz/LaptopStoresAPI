using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseModel> Add(ProductResquestModel request);
        Task Delete(int id);
        List<ProductResquestModel> GetAll();
        Task<ProductResponseModel> GetById(int id, string userId);
        Task<ProductResponseModel> Update(ProductResquestModel request);
        Task<FilterRequestModel> Filter(FilterRequestModel request);
    }
}
