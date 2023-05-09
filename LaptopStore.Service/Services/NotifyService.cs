using AutoMapper;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponseModels;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services
{
    public class NotifyService : INotifyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotifyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Add(NotifyRequestModel request)
        {
            try
            {
                var notify = new Notify
                {
                    Title = request.Title,
                    Message = request.Message,
                };
                var user = _unitOfWork.UserRepository.GetByPhone(request.Phone);
                if(user != null)
                {
                    notify.UserId = user.Id;
                    notify.RoleId = Guid.Empty;
                }
                else
                {
                    switch(request.RoleId)
                    {
                        case 1:
                            {
                                notify.RoleId = new Guid("116E0DEB-F72F-45CF-8EF8-423748B8E9B1");
                                break;
                            }
                        case 2:
                            {
                                notify.RoleId = new Guid("A1D06430-35AF-433A-AEFB-283F559059FB");
                                break;
                            }
                        case 3:
                            {
                                notify.RoleId = new Guid("6FD0F97A-1522-475C-ABA1-92F3CE5AEB04");
                                break;
                            }
                        default:
                            {
                                throw new Exception("Not Found Role");
                            }
                    }
                }
                notify = await _unitOfWork.NotifyRepository.AddAsync(notify);
                await _unitOfWork.SaveAsync();
                return;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                var notify = _unitOfWork.NotifyRepository.GetById(id);
                _unitOfWork.NotifyRepository.Delete(notify);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public NotifyResponseModel GetById(int id)
        {
            try
            {
                var notify = _unitOfWork.NotifyRepository.GetById(id);
                if(notify == null)
                {
                    throw new Exception("User Not Found");
                }
                var user = new User();
                var role = new Role();
                if (notify.UserId != Guid.Empty)
                {
                    user = _unitOfWork.UserRepository.GetById(notify.UserId.ToString());
                    role = _unitOfWork.RoleRepository.GetById(user.RoleId.ToString());
                }
                else
                {
                    user.Name = null;
                    user.Phone = null;
                    user.Img = null;
                    role = _unitOfWork.RoleRepository.GetById(notify.RoleId.ToString());
                }
                return new NotifyResponseModel
                {
                    Id = notify.Id,
                    Name = user.Name,
                    Phone = user.Phone,
                    Role = role.Name,
                    Img = user.Img,
                    Title = notify.Title,
                    Message = notify.Message
                };
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<NotifyResponseModel> GetAll()
        {
            try
            {
                var notify = _unitOfWork.NotifyRepository.GetAllUserId().ToList();
                notify.AddRange(_unitOfWork.NotifyRepository.GetAllRoleId().ToList());
                return notify;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<NotifyResponseModel> Show(string _userId, string _roleId)
        {
            try
            {
                var notify = _unitOfWork.NotifyRepository.GetByUserId(_userId);
                notify.AddRange(_unitOfWork.NotifyRepository.GetByRoleId(_roleId.ToString()));
                return _mapper.Map<List<Notify>, List<NotifyResponseModel>>(notify);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
