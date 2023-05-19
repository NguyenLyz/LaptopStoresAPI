using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class GetPasswordRequestModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Otp { get; set; }
    }
}
