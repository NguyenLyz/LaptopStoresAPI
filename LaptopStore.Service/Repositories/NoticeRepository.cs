using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.Repositories.Interfaces;
using LaptopStore.Service.RequestModels;
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
        public List<Notice> GetByUserId(string _userId)
        {
            return _context.Notices.Where(x => x.UserId == new Guid(_userId)).ToList();
        }
        public List<Notice> GetByRoleId(string _roleId)
        {
            return _context.Notices.Where(x => x.RoleId == new Guid(_roleId)).ToList();
        }
    }
}
