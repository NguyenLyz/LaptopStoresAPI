using LaptopStore.Data.Models;
using LaptopStore.MoMo.MoMoSettings;
using LaptopStore.Service.Services;
using LaptopStore.Service.Services.Interfaces;
using LaptopStore.Service.UnitOfWork.Interfaces;
using LatopStore.MoMo.Models;
using LatopStore.MoMo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LatopStore.MoMo.Services
{
    public class MoMoService : IMoMoSerivce
    {

        private static readonly HttpClient client = new HttpClient();
        private readonly IUnitOfWork _unitOfWork;
        public MoMoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> QuickPay(string id, string _userId)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.GetById(id);
                var user = _unitOfWork.UserRepository.GetById(_userId);
                if (user.Id != order.UserId) throw new Exception("");

                string accessKey = MoMoSettings.ACCESS_KEY;
                string secretKey = MoMoSettings.SECRET_KEY;

                QuickPayRequest request = new QuickPayRequest();
                request.orderInfo = MoMoSettings.ORDER_INFO;    
                request.partnerCode = MoMoSettings.PARTNER_CODE;
                request.redirectUrl = MoMoSettings.REDIRECT_URL;
                request.ipnUrl = MoMoSettings.IPN_URL;
                request.amount = order.OrderValue;
                request.orderId = order.Id;
                request.requestId = order.Id;
                request.requestType = MoMoSettings.REQUEST_TYPE;
                request.extraData = "";
                request.partnerName = MoMoSettings.PARTNER_NAME;
                request.storeId = MoMoSettings.STORE_ID;
                request.orderGroupId = "";
                request.autoCapture = MoMoSettings.AUTO_CAPTURE;
                request.lang = MoMoSettings.LANGUAGE;

                var rawSignature = "accessKey=" + accessKey +
                                    "&amount=" + request.amount +
                                    "&extraData=" + request.extraData +
                                    "&ipnUrl=" + request.ipnUrl +
                                    "&orderId=" + request.orderId +
                                    "&orderInfo=" + request.orderInfo +
                                    "&partnerCode=" + request.partnerCode +
                                    "&redirectUrl=" + request.redirectUrl +
                                    "&requestId=" + request.requestId +
                                    "&requestType=" + request.requestType;
                request.signature = getSignature(rawSignature, secretKey);
                StringContent httpContent = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json");
                var quickPayResponse = await client.PostAsync("https://test-payment.momo.vn/v2/gateway/api/create", httpContent);
                var contents = quickPayResponse.Content.ReadAsStringAsync().Result;
                var paymentresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<MoMoResponse>(contents);
                return paymentresponse.PayUrl;
            }
            catch
            {
                throw new Exception("");
            }
        }

        public string getSignature(string text, string key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        public string signSHA256(string message, string key)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string hex = BitConverter.ToString(hashmessage);
                hex = hex.Replace("-", "").ToLower();
                return hex;

            }
        }
        public async Task ConfirmResponse(MoMoRequest request)
        {
            if (request.resultCode == 0)
            {
                //success
                var trans = _unitOfWork.TransactionRepository.GetById(request.orderId);
                trans.Status = 2;
                trans.IsPay = true;
                trans.Message = request.message;
                var order = _unitOfWork.OrderRepository.GetById(request.orderId);
                order.Status = 1;
                _unitOfWork.TransactionRepository.Update(trans);
                _unitOfWork.OrderRepository.Update(order);
                _unitOfWork.ProductRepository.SuccessfulProcessing(request.orderId);
                await _unitOfWork.SaveAsync();
            }
            else
            {
                throw new Exception("");
            }
        }

    }


}
