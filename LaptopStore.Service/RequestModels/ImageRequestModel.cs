using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class ImageRequestModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Link { get; set; }
    }
}
