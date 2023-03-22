using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface ISeriesService
    {
        Task<SeriesRequestModel> Add(SeriesRequestModel request);
        Task Delete(int id);
        List<SeriesRequestModel> GetAll();
        SeriesRequestModel GetById(int id);
        Task<SeriesRequestModel> Update(SeriesRequestModel request);
    }
}
