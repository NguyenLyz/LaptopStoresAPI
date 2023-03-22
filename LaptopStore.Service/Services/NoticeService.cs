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
                notice = await _unitOfWork.NoticeRepository.AddAsync(notice);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Notice, NoticeRequestModel>(notice);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<NoticeRequestModel> Update(NoticeRequestModel request)
        {
            try
            {
                var notice = _mapper.Map<NoticeRequestModel, Notice>(request);
                notice = _unitOfWork.NoticeRepository.Update(notice);
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
    }
}
