using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Img { get; set; }
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public List<UserBehaviorTracker> UserBehaviorTrackers { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
    }
}
