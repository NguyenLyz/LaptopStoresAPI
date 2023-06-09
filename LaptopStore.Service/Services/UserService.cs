﻿using AutoMapper;
using BCrypt.Net;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Add(UserRequestModel request)
        {
            try
            {
                if(_unitOfWork.UserRepository.GetByPhone != null && _unitOfWork.UserRepository.GetByEmail != null)
                {
                    var role = Guid.NewGuid();
                    switch(request.RoleId)
                    {
                        case 1:
                            role = new Guid("a1d06430-35af-433a-aefb-283f559059fb");
                            break;
                        case 2:
                            role = new Guid("6fd0f97a-1522-475c-aba1-92f3ce5aeb04");
                            break ;
                    }
                    var user = new User
                    {
                        Name = request.Name,
                        Email = request.Email,
                        Phone = request.Phone,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                        RoleId = role,
                    };
                    user = await _unitOfWork.UserRepository.AddAsync(user);
                    await _unitOfWork.SaveAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> UpdateRole(UserRequestModel request, string _userId)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetByPhone(request.Phone);
                if(user == null || user.RoleId == new Guid("116E0DEB-F72F-45CF-8EF8-423748B8E9B1") || user.Phone == "123456" || user.Id == new Guid(_userId))
                {
                    return false;
                }
                switch(request.RoleId)
                {
                    case 1:
                        user.RoleId = new Guid("a1d06430-35af-433a-aefb-283f559059fb");
                        break;
                    case 2:
                        user.RoleId = new Guid("6fd0f97a-1522-475c-aba1-92f3ce5aeb04");
                        break;
                    default:
                        return false;
                }
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> Delete(UserRequestModel request, string _userId)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetByPhone(request.Phone);
                if(user == null || user.Phone == "123456" || user.Id == new Guid(_userId))
                {
                    return false;
                }
                _unitOfWork.UserRepository.Delete(user);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<AuthRequestModel> GetAll()
        {
            try
            {
                var query = _unitOfWork.UserRepository.GetAll();/*
                query = query.Where(x => x.RoleId.ToString() != ("116e0deb-f72f-45cf-8ef8-423748b8e9b1"));*/
                var user = query.ToList();
                var result = _mapper.Map<List<User>, List<AuthRequestModel>>(user.ToList());
                for(var i = 0; i < user.Count(); i++)
                {
                    string role = "";
                    string roleId = user[i].RoleId.ToString();
                    switch (roleId)
                    {
                        case "116e0deb-f72f-45cf-8ef8-423748b8e9b1":
                            role = "Customer";
                            break;
                        case "6fd0f97a-1522-475c-aba1-92f3ce5aeb04":
                            role = "Admin";
                            break;
                        case "a1d06430-35af-433a-aefb-283f559059fb":
                            role = "Employee";
                            break ;
                    }
                    result[i].Role = role;
                }
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public User GetByPhone(string phone)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetByPhone(phone);
                return user;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public User GetByEmail(string email)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetByEmail(email);
                return user;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public User GetById(string id)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetById(id);
                return user;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
