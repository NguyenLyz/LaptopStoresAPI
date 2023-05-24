using AutoMapper;
using Azure.Core;
using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using LaptopStore.Service.ResponseModels;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.ServiceSetting;
using LaptopStore.Service.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<JwTTokenResponseModel> Login(LoginRequestModel request)
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
                    Expires = DateTime.Now.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = _unitOfWork.JWTTokenRepository.GenerateRefreshToken();
                var tokenData = _unitOfWork.JWTTokenRepository.GetByUserId(_user.Id.ToString());
                if (tokenData == null)
                {
                    var newToken = new JwTToken
                    {
                        UserId = _user.Id,
                        AccessToken = tokenHandler.WriteToken(token),
                        RefreshToken = refreshToken,
                        RefreshTokenExpiryTime = DateTime.Now.AddDays(2),
                    };
                    await _unitOfWork.JWTTokenRepository.AddAsync(newToken);
                }
                else
                {
                    tokenData.AccessToken = tokenHandler.WriteToken(token);
                    tokenData.RefreshToken = refreshToken;
                    tokenData.RefreshTokenExpiryTime = DateTime.Now.AddDays(2);
                    _unitOfWork.JWTTokenRepository.Update(tokenData);
                }
                await _unitOfWork.SaveAsync();
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
                return new JwTTokenResponseModel
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    RefreshToken = refreshToken,
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
        public async Task<JwTTokenResponseModel> Register(string otp, RegisterRequestModel request)
        {
            try
            {
                if (_unitOfWork.UserRepository.GetByPhone(request.Phone) != null)
                    throw new Exception("Phone number is exsist");

                if (_unitOfWork.UserRepository.GetByEmail(request.Email) != null)
                    throw new Exception("Email is exsist");

                var timenow = DateTime.Now;
                var otpData = _unitOfWork.OTPRepository.GetByOTP(otp);

                if (otpData.Otpcode != otp)
                    throw new Exception("Otp incorrect");
                if (timenow.Subtract(otpData.TimeStamp) >= TimeSpan.FromSeconds(30))
                    throw new Exception("Otp time out");
                
                if (request.Name != otpData.Name || request.Email != otpData.Email || request.Phone != otpData.Phone || !BCrypt.Net.BCrypt.Verify(request.Password, otpData.Password))
                    throw new Exception("User not correct");

                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    RoleId = new Guid("116e0deb-f72f-45cf-8ef8-423748b8e9b1"),
                };
                _context.Users.Add(user);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.MobilePhone, user.Phone.ToString()),
                        new Claim(ClaimTypes.Role, user.RoleId.ToString())
                    }),
                    Expires = DateTime.Now.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken = _unitOfWork.JWTTokenRepository.GenerateRefreshToken();
                var newToken = new JwTToken
                {
                    UserId = user.Id,
                    AccessToken = tokenHandler.WriteToken(token),
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(2),
                };
                await _unitOfWork.JWTTokenRepository.AddAsync(newToken);
                _unitOfWork.OTPRepository.Delete(otpData);
                await _unitOfWork.SaveAsync();
                string role = "";
                if (user.RoleId.ToString() == "116e0deb-f72f-45cf-8ef8-423748b8e9b1")
                {
                    role = "customer";
                }
                return new JwTTokenResponseModel
                {
                    AccessToken = tokenHandler.WriteToken(token),
                    RefreshToken = refreshToken,
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
        public AuthResponseModel GetProfile(string userId)
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
                return new AuthResponseModel
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone,
                    Img = user.Img,
                    Address = user.Address,
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
                var user = _unitOfWork.UserRepository.GetByEmail(request.Email);
                if(user == null)
                {
                    return false;
                }
                var otpRequest = new OTPRequestModel
                {
                    Email = user.Email,
                    Name = user.Name,
                    Subject = "OTP Renew Password",
                    Body = "<p>OTP renew password : ",
                };
                var otpCode = SendOTP(otpRequest);
                var otpData = new OTP();
                var otp = _unitOfWork.OTPRepository.GetByEmail(user.Email);
                if (otp != null)
                {
                    otpData = otp;
                    otpData.Otpcode = otpCode;
                    otpData.TimeStamp = DateTime.Now;
                    _unitOfWork.OTPRepository.Update(otpData);
                }
                else
                {
                    otpData.Name = user.Name;
                    otpData.Email = user.Email;
                    otpData.Phone = user.Phone;
                    otpData.Password = "";
                    otpData.Otpcode = otpCode;
                    otpData.TimeStamp = DateTime.Now;
                    await _unitOfWork.OTPRepository.AddAsync(otpData);
                };
                await _unitOfWork.SaveAsync();
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
                var timenow = DateTime.Now;
                var otpData = _unitOfWork.OTPRepository.GetByOTP(request.Otp);

                if(otpData.Email != request.Email)
                    throw new Exception("Incorrect Email");

                if (otpData.Otpcode != request.Otp)
                    throw new Exception("Incorrect Otp");

                if(timenow.Subtract(otpData.TimeStamp) >= TimeSpan.FromMinutes(3))
                    throw new Exception("Otp time out");

                var user = _unitOfWork.UserRepository.GetByEmail(request.Email);
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                _context.Update(user);
                _unitOfWork.OTPRepository.Delete(otpData);
                await _context.SaveChangesAsync();
                return;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<JwTTokenResponseModel> RefreshToken(RefreshRequestModel request)
        {
            if(request == null)
            {
                throw new Exception("");
            }
            var principal = _unitOfWork.JWTTokenRepository.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null)
            {
                throw new Exception("");
            }
            string userName = principal.Identity.Name;
            var _user = _unitOfWork.UserRepository.GetById(userName);
            var tokenData = _unitOfWork.JWTTokenRepository.GetByUserId(userName);
            if (_user == null || tokenData == null || tokenData.AccessToken != request.AccessToken || tokenData.RefreshToken != request.RefreshToken || tokenData.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new Exception("");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var newAccessToken = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, _user.Id.ToString()),
                        new Claim(ClaimTypes.MobilePhone, _user.Phone.ToString()),
                        new Claim(ClaimTypes.Role, _user.RoleId.ToString())
                    }),
                Expires = DateTime.Now.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(newAccessToken);
            var refreshToken = _unitOfWork.JWTTokenRepository.GenerateRefreshToken();
            tokenData.AccessToken = tokenHandler.WriteToken(token);
            tokenData.RefreshToken = refreshToken;
            _unitOfWork.JWTTokenRepository.Update(tokenData);
            await _unitOfWork.SaveAsync();
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
            return new JwTTokenResponseModel
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken,
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
        public async Task UpdateName(AuthRequestModel request, string _userId)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetById(_userId);
                if (user == null)
                {
                    throw new Exception("User not Found");
                }
                user.Name = request.Name;
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task SendOTPwithemail(RegisterRequestModel request)
        {
            try
            {
                if (_unitOfWork.UserRepository.GetByPhone(request.Phone) != null)
                    throw new Exception("Phone number is exsist");

                if (_unitOfWork.UserRepository.GetByEmail(request.Email) != null)
                    throw new Exception("Email is exsist");

                if (_unitOfWork.OTPRepository.GetByPhone(request.Phone) != null)
                {
                    _unitOfWork.OTPRepository.Delete(_unitOfWork.OTPRepository.GetByPhone(request.Phone));
                    await _unitOfWork.SaveAsync();
                }

                if (_unitOfWork.OTPRepository.GetByEmail(request.Email) != null)
                {
                    _unitOfWork.OTPRepository.Delete(_unitOfWork.OTPRepository.GetByEmail(request.Email));
                    await _unitOfWork.SaveAsync();
                }
                var otpRequest = new OTPRequestModel
                {
                    Email = request.Email,
                    Name = request.Name,
                    Subject = "Đăng ký tài khoản người dùng",
                    Body = "<p>Mã kích hoạt : ",
                };
                var otpCode = SendOTP(otpRequest);
                var otpData = new OTP
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Otpcode = otpCode,
                    TimeStamp = DateTime.Now,
                };
                await _unitOfWork.OTPRepository.AddAsync(otpData);
                await _unitOfWork.SaveAsync();
                return;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private string SendOTP(OTPRequestModel request)
        {
            try
            {
                Random random = new Random();
                var random_otp = random.Next(100000, 999999);
                var login_otp = random_otp.ToString();

                MailMessage mail = new MailMessage();
                mail.To.Add(request.Email);
                mail.From = new MailAddress("shirokynx@gmail.com");
                mail.Subject = request.Subject;

                string email_body = "";
                email_body = "<h1>Xin chào " + request.Name + ",</h1>";
                email_body += request.Body + login_otp + "</p>";

                mail.Body = email_body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("shirokynx@gmail.com", ServiceSettings.ACCESS_PASSEMAIL);
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Send(mail);
                return login_otp;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
