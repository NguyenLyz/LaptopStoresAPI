using LaptopStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class OrderRequestModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderValue { get; set; }
        public int Status { get; set; }
        public string ShipName { get; set; }
        public string ShipPhone { get; set; }
        public string ShipAddress { get; set; }
        public string Note { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
