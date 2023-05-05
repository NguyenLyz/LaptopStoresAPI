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
    public interface INotiFyRepository : IIntF1GenericRepository<Notify>
    {
        List<Notify> GetByUserId(string _userId);
        List<Notify> GetByRoleId(string _userId);
        IQueryable<NotifyResponseModel> GetAllUserId();
        IQueryable<NotifyResponseModel> GetAllRoleId();
    }
}
