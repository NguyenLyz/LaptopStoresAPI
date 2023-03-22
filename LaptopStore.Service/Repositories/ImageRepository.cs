using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories
{
    public class ImageRepository : IntF1GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public IQueryable<Image> GetByProductId(int productid)
        {
            return _context.Images.Where(x => x.ProductId == productid);
        }
    }
}
