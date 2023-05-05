using LatopStore.MoMo.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatopStore.MoMo.Services.Interfaces
{
    public interface IMoMoSerivce
    {
        Task<string> QuickPay(string id, string _userId);
        string getSignature(string text, string key);
        string signSHA256(string message, string key);
        Task ConfirmResponse(MoMoRequest request);
    }
}
