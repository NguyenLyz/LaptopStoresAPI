using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class CartRequestModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
