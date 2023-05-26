using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Models
{
    public class ShipperOrder
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public Order Order { get; set; }
    }
}
