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
        Task<ProductResponeModel> Add(ProductResquestModel request);
        Task Delete(int id);
        List<ProductResquestModel> GetAll();
        ProductResponeModel GetById(int id);
        Task<ProductResponeModel> Update(ProductResquestModel request);
    }
}
