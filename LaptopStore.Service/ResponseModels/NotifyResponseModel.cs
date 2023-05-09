using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.ResponseModels
{
    public class NotifyResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
