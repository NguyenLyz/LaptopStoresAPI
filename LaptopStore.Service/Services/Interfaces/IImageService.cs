using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageRequestModel> Add(ImageRequestModel request);
        Task Delete(int id);
        List<ImageRequestModel> GetAll();
        ImageRequestModel GetById(int id);
        List<ImageRequestModel> GetByProductId(int productId);
        Task<ImageRequestModel> Update(ImageRequestModel request);
    }
}
