using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface IAuthService
    {
        Task ChangePassword(ChangePasswordRequestModel request, string userId);
        JwTToken Login(LoginRequestModel request);
        JwTToken Register(RegisterRequestModel request);
        AuthRequestModel GetProfile(string userId);
        Task UpdateImg(UpdateImageRequest request, string userId);
        Task<bool> CheckUser(CheckUserRequestModel request);
        Task GetPassword(GetPasswordRequestModel request);
    }
}
