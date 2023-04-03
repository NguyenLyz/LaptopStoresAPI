using AutoMapper;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
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
        public async Task<NoticeRequestModel> Add(NoticeRequestModel request)
        {
            try
            {
                var notice = _mapper.Map<NoticeRequestModel, Notice>(request);
                var user = _unitOfWork.UserRepository.GetByPhone(request.Phone);
                if(user != null)
                {
                    notice.UserId = user.Id;
                    notice.RoleId = Guid.Empty;
                }
                else
                {
                    if (request.RoleId != new Guid("6fd0f97a-1522-475c-aba1-92f3ce5aeb04") && request.RoleId != new Guid("116e0deb-f72f-45cf-8ef8-423748b8e9b1") && request.RoleId != new Guid("a1d06430-35af-433a-aefb-283f559059fb"))
                    {
                        throw new Exception("Fail to Check");
                    }
                }
                notice = await _unitOfWork.NoticeRepository.AddAsync(notice);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Notice, NoticeRequestModel>(notice);
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
        public NoticeRequestModel GetById(int id)
        {
            try
            {
                var notice = _unitOfWork.NoticeRepository.GetById(id);
                return _mapper.Map<Notice, NoticeRequestModel>(notice);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<NoticeRequestModel> GetAll()
        {
            try
            {
                var notice = _unitOfWork.NoticeRepository.GetAll();
                return _mapper.Map<List<Notice>, List<NoticeRequestModel>>(notice.ToList());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<NoticeRequestModel> Show(string _userId, string _roleId)
        {
            try
            {
                var notice = _unitOfWork.NoticeRepository.GetByUserId(_userId);
                notice.AddRange(_unitOfWork.NoticeRepository.GetByRoleId(_roleId.ToString()));
                return _mapper.Map<List<Notice>, List<NoticeRequestModel>>(notice);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
