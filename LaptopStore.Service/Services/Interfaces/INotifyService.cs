using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface INotifyService
    {
        Task Add(NotifyRequestModel request);
        Task Delete(int id);
        List<NotifyResponseModel> GetAll();
        NotifyResponseModel GetById(int id);
        //Task<NoticeRequestModel> Update(NoticeRequestModel request);
        List<NotifyResponseModel> Show(string _userId, string _roleId);
    }
}
