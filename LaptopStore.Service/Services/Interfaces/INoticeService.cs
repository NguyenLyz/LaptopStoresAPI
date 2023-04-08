using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface INoticeService
    {
        Task Add(NoticeRequestModel request);
        Task Delete(int id);
        List<NoticeResponseModel> GetAll();
        NoticeResponseModel GetById(int id);
        //Task<NoticeRequestModel> Update(NoticeRequestModel request);
        List<NoticeResponseModel> Show(string _userId, string _roleId);
    }
}
