using LaptopStore.Data.Models;
using LaptopStore.Service.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface ICommentRepository : IIntF1GenericRepository<Comment>
    {
        List<CommentResponseModel> GetByProductId(int productId);
    }
}
