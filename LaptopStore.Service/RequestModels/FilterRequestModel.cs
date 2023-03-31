using LaptopStore.Service.ResponeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class FilterRequestModel
    {
        public string Query { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public int TotalRow { get; set; }
        public int Sort { get; set; }
        public List<ProductResponseModel> Products { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int SeriesId { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
