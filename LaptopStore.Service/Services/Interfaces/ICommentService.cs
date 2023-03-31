using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentRequestModel> Add(CommentRequestModel request, string userId);
        List<CommentRequestModel> GetAll();/*
        Task<CommentRequestModel> Update(CommentRequestModel request);*/
        Task Delete(int id, string userId);
        List<CommentResponseModel> GetByProductId(int productId);
    }
}
