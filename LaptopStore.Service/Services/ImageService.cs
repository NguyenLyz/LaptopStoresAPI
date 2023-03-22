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
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ImageRequestModel> Add(ImageRequestModel request)
        {
            try
            {
                var image = _mapper.Map<ImageRequestModel, Image>(request);
                image = await _unitOfWork.ImageRepository.AddAsync(image);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Image, ImageRequestModel>(image);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<ImageRequestModel> Update(ImageRequestModel request)
        {
            try
            {
                var image = _mapper.Map<ImageRequestModel, Image>(request);
                image = _unitOfWork.ImageRepository.Update(image);
                await _unitOfWork.SaveAsync();// delete
                return _mapper.Map<Image, ImageRequestModel>(image);
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
                var image = _unitOfWork.ImageRepository.GetById(id);
                _unitOfWork.ImageRepository.Delete(image);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public ImageRequestModel GetById(int id)
        {
            try
            {
                var image = _unitOfWork.ImageRepository.GetById(id);
                return _mapper.Map<Image, ImageRequestModel>(image);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<ImageRequestModel> GetAll()
        {
            try
            {
                var image = _unitOfWork.ImageRepository.GetAll();
                return _mapper.Map<List<Image>, List<ImageRequestModel>>(image.ToList());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<ImageRequestModel> GetByProductId(int productId)
        {
            try
            {
                var image = _unitOfWork.ImageRepository.GetByProductId(productId);
                return _mapper.Map<List<Image>, List<ImageRequestModel>>(image.ToList());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
