using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface IProductRepository : IIntF1GenericRepository<Product>
    {
        Product GetBySlug(string slug);
        Task SuccessfulProcessing(string orderId);
        Task CancelProcessing(string orderId);
        Task<FilterRequestModel> Filter(FilterRequestModel request);
        Task<List<ProductResponseModel>> GetBestSeller();
        Task<List<ProductResponseModel>> GetNewestProduct();
    }
}
