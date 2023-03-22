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
    public class BrandService :IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BrandRequestModel> Add(BrandRequestModel request)
        {
            try
            {
                var brand = _mapper.Map<BrandRequestModel, Brand>(request);
                brand = await _unitOfWork.BrandRepository.AddAsync(brand);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Brand, BrandRequestModel>(brand);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<BrandRequestModel> Update(BrandRequestModel request)
        {
            try
            {
                var brand = _unitOfWork.BrandRepository.GetById(request.Id);
                brand.Name = request.Name;
                brand.Logo = request.Logo;
                brand.Description = request.Description;
                brand = _unitOfWork.BrandRepository.Update(brand);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Brand, BrandRequestModel>(brand);
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
                var brand = _unitOfWork.BrandRepository.GetById(id);
                _unitOfWork.BrandRepository.Delete(brand);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public BrandRequestModel GetById(int id)
        {
            try
            {
                var brand = _unitOfWork.BrandRepository.GetById(id);
                return _mapper.Map<Brand, BrandRequestModel>(brand);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<BrandRequestModel> GetAll()
        {
            try
            {
                var brand = _unitOfWork.BrandRepository.GetAll();
                return _mapper.Map<List<Brand>, List<BrandRequestModel>>(brand.ToList());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }

}
