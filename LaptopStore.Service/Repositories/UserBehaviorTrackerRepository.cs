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
    public class UserBehaviorTrackerRepository : F0GenericRepository<UserBehaviorTracker>, IUserBehaviorTrackerRepository
    {
        public UserBehaviorTrackerRepository(LaptopStoreDbContext context) : base(context)
        {
        }
    }
}
