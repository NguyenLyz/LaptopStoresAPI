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
        Task<AuthRequestModel> UpdateImg(string img, string userId);
        Task GetPhoneToChange(string phone);
        Task GetPassword(GetPasswordRequestModel request);
    }
}
