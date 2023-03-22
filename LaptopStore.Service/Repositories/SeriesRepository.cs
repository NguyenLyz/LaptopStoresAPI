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
    public class SeriesRepository : IntF1GenericRepository<Series>, ISeriesRepository
    {
        public SeriesRepository(LaptopStoreDbContext context) : base(context)
        {
        }
    }
}
