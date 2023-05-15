using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using LaptopStore.Service.ResponseModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface IAuthService
    {
        Task ChangePassword(ChangePasswordRequestModel request, string userId);
        Task<JwTTokenResponseModel> Login(LoginRequestModel request);
        Task<JwTTokenResponseModel> Register(string otp, RegisterRequestModel request);
        AuthResponseModel GetProfile(string userId);
        Task UpdateImg(UpdateImageRequest request, string userId);
        Task<bool> CheckUser(CheckUserRequestModel request);
        Task GetPassword(GetPasswordRequestModel request);
        Task<JwTTokenResponseModel> RefreshToken(RefreshRequestModel request);
        Task UpdateName(AuthRequestModel request, string _userId);
        Task SendOTPwithemail(RegisterRequestModel request);
    }
}
