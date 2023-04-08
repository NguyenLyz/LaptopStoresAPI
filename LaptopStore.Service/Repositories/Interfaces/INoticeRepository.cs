using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface INoticeRepository : IIntF1GenericRepository<Notice>
    {
        List<Notice> GetByUserId(string _userId);
        List<Notice> GetByRoleId(string _userId);
        IQueryable<NoticeResponseModel> GetAllUserId();
        IQueryable<NoticeResponseModel> GetAllRoleId();
    }
}
