using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.ResponseModels
{
    public class NoticeResponseModel
    {
        public int Id { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; }
        public string Message { get; set; }
    }
}
