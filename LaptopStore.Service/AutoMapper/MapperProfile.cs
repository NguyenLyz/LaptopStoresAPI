using AutoMapper;
using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using LaptopStore.Service.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AdvertisementRequestModel, Advertisement>().ReverseMap();
            CreateMap<BrandRequestModel, Brand>().ReverseMap();
            CreateMap<CartRequestModel,Cart>().ReverseMap();
            CreateMap<CategoryRequestModel, Category>().ReverseMap();
            CreateMap<CommentRequestModel, Comment>().ReverseMap();
            CreateMap<NotifyRequestModel, Notify>().ReverseMap();
            CreateMap<OrderRequestModel, Order>().ReverseMap();
            CreateMap<ProductResquestModel, Product>().ReverseMap();
            CreateMap<SeriesRequestModel, Series>().ReverseMap();
            CreateMap<UserRequestModel, User>().ReverseMap();
            CreateMap<AuthRequestModel, User>().ReverseMap();

            CreateMap<ProductResponseModel, Product>().ReverseMap();
            CreateMap<NotifyResponseModel, Notify>().ReverseMap();
            CreateMap<OrderResponseModel, Order>().ReverseMap();
        }
    }
}
