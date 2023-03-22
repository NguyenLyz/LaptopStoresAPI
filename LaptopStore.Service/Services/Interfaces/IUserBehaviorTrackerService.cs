using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Services.Interfaces
{
    public interface IUserBehaviorTrackerService
    {
        Task Add(string userid, int brandid, int categoryid, int seriesid);
    }
}
