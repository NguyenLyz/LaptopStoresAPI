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
    public class NotifyRepository : IntF1GenericRepository<Notify>, INotiFyRepository
    {
        public NotifyRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public List<Notify> GetByUserId(string _userId)
        {
            return _context.Notices.Where(x => x.UserId == new Guid(_userId)).ToList();
        }
        public List<Notify> GetByRoleId(string _roleId)
        {
            return _context.Notices.Where(x => x.RoleId == new Guid(_roleId)).ToList();
        }
        public IQueryable<NotifyResponseModel> GetAllUserId()
        {
            var query = from notify in _context.Notices
                        join user in _context.Users on notify.UserId equals user.Id
                        join role in _context.Roles on user.RoleId equals role.Id
                        select new NotifyResponseModel
                        {
                            Id = notify.Id,
                            Name = user.Name,
                            Phone = user.Phone,
                            Role = role.Name,
                            Img = user.Img,
                            Title = notify.Title,
                            Message = notify.Message
                        };
            return query;
        }
        public IQueryable<NotifyResponseModel> GetAllRoleId()
        {
            var query = from notify in _context.Notices
                        join role in _context.Roles on notify.RoleId equals role.Id
                        select new NotifyResponseModel
                        {
                            Id = notify.Id,
                            Phone = null,
                            Role = role.Name,
                            Title = notify.Title,
                            Message = notify.Message
                        };
            return query;
        }
    }
}
