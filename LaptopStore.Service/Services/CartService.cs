using AutoMapper;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<CartRequestModel> Add(CartRequestModel request, string _userId)
        {
            try
            {
                var _cart = _unitOfWork.CartRepository.GetById(_userId, request.ProductId);
                if(_cart == null)
                {
                    if(_unitOfWork.ProductRepository.GetById(request.ProductId).Available >= request.Quantity)
                    {
                        var cart = _mapper.Map<CartRequestModel, Cart>(request);
                        cart.UserId = new Guid(_userId);
                        _cart = await _unitOfWork.CartRepository.AddAsync(cart);
                    }
                }
                else
                {
                    if(_unitOfWork.ProductRepository.GetById(_cart.ProductId).Available >= _cart.Quantity + request.Quantity)
                    {
                        _cart.Quantity = _cart.Quantity + request.Quantity;
                        _cart = _unitOfWork.CartRepository.Update(_cart);
                    }
                }
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Cart, CartRequestModel>(_cart);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<CartRequestModel> Update(CartRequestModel request, string _userId)
        {
            try
            {
                var cart = _unitOfWork.CartRepository.GetById(_userId, request.ProductId);
                cart.Quantity = request.Quantity;
                cart = _unitOfWork.CartRepository.Update(cart);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<Cart, CartRequestModel>(cart);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task Delete(CartRequestModel request, string userId)
        {
            try
            {
                var cart = _unitOfWork.CartRepository.GetById(userId, request.ProductId);
                _unitOfWork.CartRepository.Delete(cart);
                await _unitOfWork.SaveAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<List<CartResponseModel>> GetByUserId(string _userId)
        {
            try
            {
                var carts = _unitOfWork.CartRepository.GetByUserId(_userId);
                var result = _mapper.Map<List<CartResponseModel>>(carts);
                foreach (var cart in result)
                {
                    cart.Product = await _productService.GetById(cart.ProductId, null);
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
