using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class NoticeRequestModel
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string Message { get; set; }
    }
}
