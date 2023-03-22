using LaptopStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class ProductResquestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int SeriesId { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public int Available { get; set; }
        public List<ImageRequestModel> Images { get; set; }
    }
}
