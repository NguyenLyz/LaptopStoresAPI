using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Models
{
    public class UserBehaviorTracker
    {
        public Guid UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int SeriesId { get; set; }

        public User User { get; set; }
        public Brand Brand { get; set; }
        public Category Category { get; set; }
        public Series Series { get; set; }
    }
}
