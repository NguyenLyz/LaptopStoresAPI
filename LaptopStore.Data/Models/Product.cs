using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LaptopStore.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int SeriesId { get; set; }
        public string Tags { get; set; }
        public string Images { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Sold { get; set; }
        public int Available { get; set; }

        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public Series Series { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
