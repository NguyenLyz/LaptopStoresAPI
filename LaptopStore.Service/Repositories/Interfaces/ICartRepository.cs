using LaptopStore.Data.Models;
using LaptopStore.Service.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface ICartRepository : IF0GenericRepository<Cart>
    {
        Cart GetById(string _userId, int productId);
        List<CartResponseModel> GetByUserId(string _userId);
        void DeleteByUserId(string _userId);
    }
}
