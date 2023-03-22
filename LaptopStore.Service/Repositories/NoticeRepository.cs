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
    public class NoticeRepository : IntF1GenericRepository<Notice>, INoticeRepository
    {
        public NoticeRepository(LaptopStoreDbContext context) : base(context)
        {
        }
    }
}
