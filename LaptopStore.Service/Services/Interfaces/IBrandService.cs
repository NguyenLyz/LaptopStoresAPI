using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface IBrandService
    {
        Task<BrandRequestModel> Add(BrandRequestModel request);
        Task Delete(int id);
        List<BrandRequestModel> GetAll();
        BrandRequestModel GetById(int id);
        Task<BrandRequestModel> Update(BrandRequestModel request);
    }
}
