using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatopStore.MoMo.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public string Currency { get; set; }
        public int Quantity { get; set; }
        public long TotalPrice { get; set; }
    }
}
