﻿using LaptopStore.Service.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.ResponeModels
{
    public class JwTTokenResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public AuthRequestModel User { get; set; }
    }
}
