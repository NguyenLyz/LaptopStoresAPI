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
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdvertisementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AdvertisementRequestModel> Add(AdvertisementRequestModel request)
        {
            try
            {
                var advertisement = _mapper.Map<AdvertisementRequestModel, Advertisement>(request);
                advertisement = await _unitOfWork.AdvertisementRepository.AddAsync(advertisement);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Advertisement, AdvertisementRequestModel>(advertisement);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<AdvertisementRequestModel> Update(AdvertisementRequestModel request)
        {
            try
            {
                var advertisement = _unitOfWork.AdvertisementRepository.GetById(request.Id);
                advertisement.Image = request.Img;
                advertisement.Link = request.Link;
                advertisement = _unitOfWork.AdvertisementRepository.Update(advertisement);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Advertisement, AdvertisementRequestModel>(advertisement);
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
                var advertisement = _unitOfWork.AdvertisementRepository.GetById(id);
                _unitOfWork.AdvertisementRepository.Delete(advertisement);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public AdvertisementRequestModel GetById(int id)
        {
            try
            {
                var advertisement = _unitOfWork.AdvertisementRepository.GetById(id);
                return _mapper.Map<Advertisement, AdvertisementRequestModel>(advertisement);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<AdvertisementRequestModel> GetAll()
        {
            try
            {
                var advertisement = _unitOfWork.AdvertisementRepository.GetAll();
                return _mapper.Map<List<Advertisement>, List<AdvertisementRequestModel>>(advertisement.ToList());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
