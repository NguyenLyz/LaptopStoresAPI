using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentRequestModel> Add(CommentRequestModel request);
        List<CommentRequestModel> GetAll();
        Task<CommentRequestModel> Update(CommentRequestModel request);
    }
}
