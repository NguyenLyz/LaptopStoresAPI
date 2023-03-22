using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Models
{
    public class JwTToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
