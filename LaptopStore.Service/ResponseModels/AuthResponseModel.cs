using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.ResponseModels
{
    public class AuthResponseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Img { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }
}
