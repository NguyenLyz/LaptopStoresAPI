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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CategoryRequestModel> Add(CategoryRequestModel request)
        {
            try
            {
                var category = _mapper.Map<CategoryRequestModel, Category>(request);
                category = await _unitOfWork.CategoryRepository.AddAsync(category);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Category, CategoryRequestModel>(category);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<CategoryRequestModel> Update(CategoryRequestModel request)
        {
            try
            {
                var category = _unitOfWork.CategoryRepository.GetById(request.Id);
                if (category == null)
                {
                    throw new Exception("Category not Found");
                }
                category.Name = request.Name;
                category.Description = request.Description;
                category = _unitOfWork.CategoryRepository.Update(category);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Category, CategoryRequestModel>(category);
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
                var category = _unitOfWork.CategoryRepository.GetById(id);
                _unitOfWork.CategoryRepository.Delete(category);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public CategoryRequestModel GetById(int id)
        {
            try
            {
                var category = _unitOfWork.CategoryRepository.GetById(id);
                return _mapper.Map<Category, CategoryRequestModel>(category);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<CategoryRequestModel> GetAll()
        {
            try
            {
                var category = _unitOfWork.CategoryRepository.GetAll();
                return _mapper.Map<List<Category>, List<CategoryRequestModel>>(category.ToList());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
