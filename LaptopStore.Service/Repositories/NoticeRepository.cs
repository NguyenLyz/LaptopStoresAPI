using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.Repositories.Interfaces;
using LaptopStore.Service.RequestModels;
using LaptopStore.Service.ResponseModels;
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
        public IQueryable<NoticeResponseModel> GetAllUserId()
        {
            var query = from notice in _context.Notices
                        join user in _context.Users on notice.UserId equals user.Id
                        join role in _context.Roles on user.RoleId equals role.Id
                        select new NoticeResponseModel
                        {
                            Id = notice.Id,
                            Phone = user.Phone,
                            Role = role.Name,
                            Message = notice.Message
                        };
            return query;
        }
        public IQueryable<NoticeResponseModel> GetAllRoleId()
        {
            var query = from notice in _context.Notices
                        join role in _context.Roles on notice.RoleId equals role.Id
                        select new NoticeResponseModel
                        {
                            Id = notice.Id,
                            Phone = null,
                            Role = role.Name,
                            Message = notice.Message
                        };
            return query;
        }
    }
}
