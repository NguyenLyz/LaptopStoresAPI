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
    public class AdvertisementRepository : IntF1GenericRepository<Advertisement> ,IAdvertisementRepository
    {
        public AdvertisementRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public List<Advertisement> Show()
        {
            return _context.Advertisements.OrderByDescending(x => x.Id).Take(5).ToList();
        }
    }
}
