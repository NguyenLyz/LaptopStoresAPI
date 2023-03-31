using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface INoticeService
    {
        Task<NoticeRequestModel> Add(NoticeRequestModel request);
        Task Delete(int id);
        List<NoticeRequestModel> GetAll();
        NoticeRequestModel GetById(int id);
        //Task<NoticeRequestModel> Update(NoticeRequestModel request);
    }
}
