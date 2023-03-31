using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.ResponeModels
{
    public class CartResponseModel
    {
        public int ProductId { get; set; }
        public ProductResponseModel Product { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public int Price { get; set; }
    }
}
