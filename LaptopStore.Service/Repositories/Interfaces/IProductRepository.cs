using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface IProductRepository : IIntF1GenericRepository<Product>
    {
        Task SuccessfulProcessing(int orderId);
        Task CancelProcessing(int orderId);
        Task<FilterRequestModel> Filter(FilterRequestModel request);
    }
}
