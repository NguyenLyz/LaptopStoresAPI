using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Models
{
    public class Notice
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
