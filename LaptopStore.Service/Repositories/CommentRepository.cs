using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.Repositories.Interfaces;
using LaptopStore.Service.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories
{
    public class CommentRepository : IntF1GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public List<CommentResponseModel> GetByProductId(int productId)
        {
            var query = from comment in _context.Comments
                        join user in _context.Users on comment.UserId equals user.Id
                        where comment.ProductId == productId
                        select new CommentResponseModel
                        {
                            Id = comment.Id,
                            UserName = user.Name,
                            Content = comment.Content
                        };
            return query.ToList();
        }
    }
}
