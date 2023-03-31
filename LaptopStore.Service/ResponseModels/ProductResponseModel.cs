using LaptopStore.Data.Models;
using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.ResponeModels
{
    public class ProductResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Brand { get; set; }
        public int BrandId { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string Series { get; set; }
        public int SeriesId { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Images { get; set; }
        public int Sold { get; set; }
        public int Available { get; set; }
    }
}
