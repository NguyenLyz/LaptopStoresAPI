using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryRequestModel> Add(CategoryRequestModel request);
        Task Delete(int id);
        List<CategoryRequestModel> GetAll();
        CategoryRequestModel GetById(int id);
        Task<CategoryRequestModel> Update(CategoryRequestModel request);
    }
}
