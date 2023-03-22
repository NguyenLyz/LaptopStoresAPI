using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class NoticeRequestModel
    {
        public Guid UserId { get; set; }
        public string Message { get; set; }
    }
}
