using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatopStore.MoMo.Settings
{
    public class Settings
    {
        public const string ACCESS_KEY = "F8BBA842ECF85";
        public const string SECRET_KEY = "K951B6PE1waDMi640xX08PD3vg6EkVlz";

        public const string ORDER_INFO = "pay with MoMo";
        public const string PARTNER_CODE = "MOMO";
        public const string IPN_URL = "https://localhost:7296/api/Order/momo-payment-request";
        public const string REDIRECT_URL = "https://localhost:7296/api/Order/momo-payment-request";
        public const string REQUEST_TYPE = "payWithMethod";
        public const string PARTNER_NAME = "MoMo Payment";
        public const string STORE_ID = "BookStore";
        public const bool AUTO_CAPTURE = true;
        public const string LANGUAGE = "vi";

        public const string CREATE_URL = "https://test-payment.momo.vn/v2/gateway/api/create";
    }
}
