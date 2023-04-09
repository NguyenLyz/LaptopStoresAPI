using AutoMapper;
using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwTToken = LaptopStore.Service.ResponeModels.JwTToken;

namespace LaptopStore.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly LaptopStoreDbContext _context;
        private readonly IConfiguration _iconfiguration;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration iconfiguration, LaptopStoreDbContext context)
        {
            _unitOfWork = unitOfWork;
            _iconfiguration = iconfiguration;
            _context = context;
        }
        public JwTToken Login(LoginRequestModel request)
        {
            try
            {
                var _user = _unitOfWork.UserRepository.GetByPhone(request.Phone);
                if(_user == null || !BCrypt.Net.BCrypt.Verify(request.Password, _user.Password))
                {
                    return null;
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, _user.Id.ToString()),
                        new Claim(ClaimTypes.MobilePhone, _user.Phone.ToString()),
                        new Claim(ClaimTypes.Role, _user.RoleId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                string role = "";
                string roleId = _user.RoleId.ToString();
                switch (roleId)
                {
                    case "116e0deb-f72f-45cf-8ef8-423748b8e9b1":
                        role = "customer";
                        break;
                    case "6fd0f97a-1522-475c-aba1-92f3ce5aeb04":
                        role = "admin";
                        break;
                    case "a1d06430-35af-433a-aefb-283f559059fb":
                        role = "employee";
                        break;
                }
                return new JwTToken
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    User = new AuthRequestModel
                    {
                        Name = _user.Name,
                        Email = _user.Email,
                        Phone = _user.Phone,
                        Img = _user.Img,
                        Role = role,
                    }
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public JwTToken Register(RegisterRequestModel request)
        {
            try
            {
                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    RoleId = new Guid("116e0deb-f72f-45cf-8ef8-423748b8e9b1"),
                };
                if (_unitOfWork.UserRepository.GetByPhone(request.Phone) != null)
                    throw new Exception("Phone number is exsist");

                if (_unitOfWork.UserRepository.GetByEmail(request.Email) != null)
                    throw new Exception("Email is exsist");

                _context.Users.Add(user);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                _context.SaveChanges();
                string role = "";
                if (user.RoleId.ToString() == "116e0deb-f72f-45cf-8ef8-423748b8e9b1")
                {
                    role = "customer";
                }
                return new JwTToken
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    User = new AuthRequestModel
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Phone = user.Phone,
                        Role = role
                    }
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task ChangePassword(ChangePasswordRequestModel request, string userId)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetById(userId);
                if(BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Change Password Error");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public AuthRequestModel GetProfile(string userId)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetById(userId);
                string role = "";
                string roleId = user.RoleId.ToString();
                switch(roleId)
                {
                    case "116e0deb-f72f-45cf-8ef8-423748b8e9b1":
                        role = "customer";
                        break;
                    case "6fd0f97a-1522-475c-aba1-92f3ce5aeb04":
                        role = "admin";
                        break;
                    case "a1d06430-35af-433a-aefb-283f559059fb":
                        role = "employee";
                        break;
                }
                return new AuthRequestModel
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Img = user.Img,
                    Role = role
                };
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task UpdateImg(UpdateImageRequest request, string userId)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetById(userId);
                if(user == null)
                {
                    throw new Exception("User not found");
                }
                user.Img = request.Image;
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> CheckUser(CheckUserRequestModel request)
        {
            try
            {
                if(_unitOfWork.UserRepository.GetByPhone(request.Phone) == null)
                {
                    return false;
                }
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task GetPassword(GetPasswordRequestModel request)
        {
            try
            {
                if (request.Otp != "000000")
                {
                    throw new Exception("Incorrect Otp");
                }
                var user = _unitOfWork.UserRepository.GetByPhone(request.Phone);
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                _context.Update(user);
                await _context.SaveChangesAsync();
                return;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
