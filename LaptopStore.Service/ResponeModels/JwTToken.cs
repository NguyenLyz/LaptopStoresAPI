using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.ResponeModels
{
    public class JwTToken
    {
        public string AccessToken { get; set; }
        public AuthRequestModel User { get; set; }
    }
}
