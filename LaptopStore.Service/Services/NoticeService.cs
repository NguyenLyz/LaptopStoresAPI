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
    public class NoticeService : INoticeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NoticeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Add(NoticeRequestModel request)
        {
            try
            {
                var notice = new Notice
                {
                    Message = request.Message,
                };
                var user = _unitOfWork.UserRepository.GetByPhone(request.Phone);
                if(user != null)
                {
                    notice.UserId = user.Id;
                    notice.RoleId = Guid.Empty;
                }
                else
                {
                    switch(request.RoleId)
                    {
                        case 1:
                            {
                                notice.RoleId = new Guid("116E0DEB-F72F-45CF-8EF8-423748B8E9B1");
                                break;
                            }
                        case 2:
                            {
                                notice.RoleId = new Guid("A1D06430-35AF-433A-AEFB-283F559059FB");
                                break;
                            }
                        case 3:
                            {
                                notice.RoleId = new Guid("6FD0F97A-1522-475C-ABA1-92F3CE5AEB04");
                                break;
                            }
                        default:
                            {
                                throw new Exception("Not Found Role");
                            }
                    }
                }
                notice = await _unitOfWork.NoticeRepository.AddAsync(notice);
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
                var notice = _unitOfWork.NoticeRepository.GetById(id);
                _unitOfWork.NoticeRepository.Delete(notice);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public NoticeResponseModel GetById(int id)
        {
            try
            {
                var notice = _unitOfWork.NoticeRepository.GetById(id);
                var user = new User();
                var role = new Role();
                if (notice.UserId != Guid.Empty)
                {
                    user = _unitOfWork.UserRepository.GetById(notice.UserId.ToString());
                    role = _unitOfWork.RoleRepository.GetById(user.RoleId.ToString());
                }
                else
                {
                    user.Phone = null;
                    role = _unitOfWork.RoleRepository.GetById(notice.RoleId.ToString());
                }
                return new NoticeResponseModel
                {
                    Id = notice.Id,
                    Phone = user.Phone,
                    Role = role.Name,
                    Message = notice.Message
                };
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<NoticeResponseModel> GetAll()
        {
            try
            {
                var notice = _unitOfWork.NoticeRepository.GetAllUserId().ToList();
                notice.AddRange(_unitOfWork.NoticeRepository.GetAllRoleId().ToList());
                return notice;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<NoticeResponseModel> Show(string _userId, string _roleId)
        {
            try
            {
                var notice = _unitOfWork.NoticeRepository.GetByUserId(_userId);
                notice.AddRange(_unitOfWork.NoticeRepository.GetByRoleId(_roleId.ToString()));
                return _mapper.Map<List<Notice>, List<NoticeResponseModel>>(notice);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
