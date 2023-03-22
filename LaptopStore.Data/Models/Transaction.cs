using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Models
{
    public class Transaction
    {
        public int OrderId { get; set; }
        public int Status { get; set; }
        public int Amount { get; set; }
        public string Message { get; set; } = string.Empty;

        public Order Order { get; set; }
    }
}
