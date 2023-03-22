using AutoMapper;
using LaptopStore.Data.Models;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services
{
    public class UserBehaviorTrackerService : IUserBehaviorTrackerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserBehaviorTrackerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Add(string userid, int brandid, int categoryid, int seriesid)
        {
            try
            {
                var userBehaviorTracker = new UserBehaviorTracker
                {
                    UserId = new Guid(userid),
                    TimeStamp = DateTime.Now,
                    BrandId = brandid,
                    CategoryId = categoryid,
                    SeriesId = seriesid
                };
                await _unitOfWork.UserBehaviorTrackerRepository.AddAsync(userBehaviorTracker);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
