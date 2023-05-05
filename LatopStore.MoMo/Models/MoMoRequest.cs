using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatopStore.MoMo.Models
{
    public class MoMoRequest
    {
        public string partnerCode { get; set; } = string.Empty;
        public string orderId { get; set; } = string.Empty;
        public string requestId { get; set; } = string.Empty;
        public long amount { get; set; }
        public string orderInfo { get; set; } = string.Empty;
        public string? partnerUserId { get; set; }
        public string orderType { get; set; } = string.Empty;
        public long transId { get; set; }
        public int resultCode { get; set; }
        public string message { get; set; } = string.Empty;
        public string payType { get; set; } = string.Empty;
        public long responseTime { get; set; }
        public string? extraData { get; set; }
        public string signature { get; set; } = string.Empty;
    }
}
