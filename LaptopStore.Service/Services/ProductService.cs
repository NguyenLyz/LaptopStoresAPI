using AutoMapper;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using Slugify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserBehaviorTrackerService _userBehaviorTrackerService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IUserBehaviorTrackerService userBehaviorTrackerService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userBehaviorTrackerService = userBehaviorTrackerService;
        }
        public async Task<ProductResponseModel> Add(ProductResquestModel request)
        {
            try
            {
                SlugHelper helper = new SlugHelper();
                var product = new Product
                {
                    Id = request.Id,
                    Name = request.Name,
                    BrandId = request.BrandId,
                    CategoryId = request.CategoryId,
                    SeriesId = request.SeriesId,
                    Price = request.Price,
                    Discount = request.Discount,
                    Description = request.Description,
                    Available = request.Available
                };
                product.Slug = helper.GenerateSlug(request.Name) + "-" + Guid.NewGuid().ToString();
                product.Tags = string.Join("$", request.Tags.ToArray());
                product.Images = string.Join("$", request.Images.ToArray());
                await _unitOfWork.ProductRepository.AddAsync(product);
                await _unitOfWork.SaveAsync();

                var brand = _unitOfWork.BrandRepository.GetById(product.BrandId);
                var category = _unitOfWork.CategoryRepository.GetById(product.CategoryId);
                var series = _unitOfWork.SeriesRepository.GetById(product.SeriesId);

                var result = new ProductResponseModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Slug = product.Slug,
                    Brand = brand.Name,
                    BrandId = product.BrandId,
                    Category = category.Name,
                    CategoryId = product.CategoryId,
                    Series = series.Name,
                    SeriesId = product.SeriesId,
                    Price = product.Price,
                    Discount = product.Discount,
                    Description= product.Description,
                    Tags = product.Tags.Split("$").ToList(),
                    Images = product.Images.Split("$").ToList(),
                    Sold = product.Sold,
                    Available = product.Available,
                };
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<ProductResponseModel> Update(ProductResquestModel request)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.GetById(request.Id);
                if (product == null) throw new Exception("Product not found");
                var product1 = _mapper.Map<ProductResquestModel, Product>(request);
                SlugHelper helper = new SlugHelper();
                product.Name = product1.Name;
                product.Slug = helper.GenerateSlug(product1.Name) + "-" + Guid.NewGuid().ToString();
                product.BrandId = product1.BrandId;
                product.CategoryId = product1.CategoryId;
                product.SeriesId = product1.SeriesId;
                product.Price = product1.Price;
                product.Discount = product1.Discount;
                product.Description = product1.Description;
                product.Tags = string.Join("$", request.Tags.ToArray());
                product.Images = string.Join("$", request.Images.ToArray());
                product.Available = product1.Available;
                _unitOfWork.ProductRepository.Update(product);
                await _unitOfWork.SaveAsync();

                var brand = _unitOfWork.BrandRepository.GetById(product.BrandId);
                var category = _unitOfWork.CategoryRepository.GetById(product.CategoryId);
                var series = _unitOfWork.SeriesRepository.GetById(product.SeriesId);

                var result = new ProductResponseModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Slug = product.Slug,
                    Brand = brand.Name,
                    BrandId = product.BrandId,
                    Category = category.Name,
                    CategoryId = product.CategoryId,
                    Series = series.Name,
                    SeriesId = product.SeriesId,
                    Price = product.Price,
                    Discount = product.Discount,
                    Description = product.Description,
                    Tags = product.Tags.Split("$").ToList(),
                    Images = product.Images.Split("$").ToList(),
                    Sold = product.Sold,
                    Available = product.Available,
                };
                return result;
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
                var product = _unitOfWork.ProductRepository.GetById(id);
                if (product == null) throw new Exception("Product not found");
                _unitOfWork.ProductRepository.Delete(product);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<ProductResponseModel> GetById(int id, string userId)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.GetById(id);
                var brand = _unitOfWork.BrandRepository.GetById(product.BrandId).Name;
                var category = _unitOfWork.CategoryRepository.GetById(product.CategoryId).Name;
                var series = _unitOfWork.SeriesRepository.GetById(product.SeriesId).Name;
                var result = new ProductResponseModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Slug = product.Slug,
                    Brand = brand,
                    BrandId = product.BrandId,
                    Category = category,
                    CategoryId = product.CategoryId,
                    Series = series,
                    SeriesId = product.SeriesId,
                    Price = product.Price,
                    Discount = product.Discount,
                    Description = product.Description,
                    Sold = product.Sold,
                    Available = product.Available
                };
                result.Tags = product.Tags.Split("$").ToList();
                result.Images = product.Images.Split("$").ToList();
                if(userId != null)
                {
                    await _userBehaviorTrackerService.Add(userId, result.BrandId, result.CategoryId, result.SeriesId);
                }
                return result;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<ProductResponseModel> GetBySlug(string slug, string userId)
        {
            try
            {
                var product = _unitOfWork.ProductRepository.GetBySlug(slug);
                var brand = _unitOfWork.BrandRepository.GetById(product.BrandId).Name;
                var category = _unitOfWork.CategoryRepository.GetById(product.CategoryId).Name;
                var series = _unitOfWork.SeriesRepository.GetById(product.SeriesId).Name;
                var result = new ProductResponseModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Slug = product.Slug,
                    Brand = brand,
                    BrandId = product.BrandId,
                    Category = category,
                    CategoryId = product.CategoryId,
                    Series = series,
                    SeriesId = product.SeriesId,
                    Price = product.Price,
                    Discount = product.Discount,
                    Description = product.Description,
                    Sold = product.Sold,
                    Available = product.Available
                };
                result.Tags = product.Tags.Split("$").ToList();
                result.Images = product.Images.Split("$").ToList();
                if (userId != null)
                {
                    await _userBehaviorTrackerService.Add(userId, result.BrandId, result.CategoryId, result.SeriesId);
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<ProductResquestModel> GetAll()
        {
            try
            {
                var product = _unitOfWork.ProductRepository.GetAll();
                return _mapper.Map<List<Product>, List<ProductResquestModel>>(product.ToList());
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<FilterRequestModel> Filter(FilterRequestModel request)
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.Filter(request);
                return products;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<List<ProductResponseModel>> ShowBestSeller()
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetBestSeller();
                return product;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<List<ProductResponseModel>> ShowNewestProduct()
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetNewestProduct();
                return product;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
