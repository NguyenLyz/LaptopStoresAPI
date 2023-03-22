using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface IAdvertisementService
    {
        Task<AdvertisementRequestModel> Add(AdvertisementRequestModel request);
        Task Delete(int id);
        List<AdvertisementRequestModel> GetAll();
        AdvertisementRequestModel GetById(int id);
        Task<AdvertisementRequestModel> Update(AdvertisementRequestModel request);
    }
}
