﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Data.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
