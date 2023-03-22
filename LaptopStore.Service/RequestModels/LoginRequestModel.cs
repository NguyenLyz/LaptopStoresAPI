using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class LoginRequestModel
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}
