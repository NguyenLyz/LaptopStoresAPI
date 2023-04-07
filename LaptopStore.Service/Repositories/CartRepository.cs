using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.Repositories.Interfaces;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponeModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories
{
    public class CartRepository : F0GenericRepository<Cart>, ICartRepository
    {
        private readonly IConfiguration _configuration;
        public CartRepository(LaptopStoreDbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }
        public Cart GetById(string _userId, int productId)
        {
            var query = from cart in _context.Carts
                        where cart.UserId == new Guid(_userId) && cart.ProductId == productId
                        select cart;
            return query.FirstOrDefault();
        }
        public List<CartResponseModel> GetByUserId(string _userId)
        {
            var query = from cart in _context.Carts
                        join product in _context.Products on cart.ProductId equals product.Id
                        where cart.UserId == new Guid(_userId)
                        select new CartResponseModel
                        {
                            ProductId = product.Id,
                            Quantity = cart.Quantity,
                        };
            return query.ToList();
        }
        public void DeleteByUserId(string _userId)
        {
            var connection = _configuration.GetConnectionString("DefaultConnection");
            SqlConnection conn = new SqlConnection(connection);
            using (var query = new SqlCommand("DELETE FROM Carts WHERE userId = @UserId", conn))
            {
                query.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = _userId;
                conn.Open();
                query.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
