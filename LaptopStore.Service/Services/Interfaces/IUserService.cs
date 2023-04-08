using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Add(UserRequestModel request);
        Task<bool> Delete(string id);
        List<AuthRequestModel> GetAll();
        User GetByEmail(string email);
        User GetById(string id);
        User GetByPhone(string phone);
        Task<bool> UpdateRole(UserRequestModel request);
    }
}
