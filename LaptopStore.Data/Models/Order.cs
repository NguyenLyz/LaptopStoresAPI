using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Models
{
    public class Order
    {
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderValue { get; set; }
        public string ShipName { get; set; } = string.Empty;
        public string ShipPhone { get; set; } = string.Empty;
        public string ShipAddress { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int ShipMethod { get; set; }
        public Guid ShipperId { get; set; }

        public User User { get; set; }
        public User Shipper { get; set; }
        public Transaction Transaction { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
