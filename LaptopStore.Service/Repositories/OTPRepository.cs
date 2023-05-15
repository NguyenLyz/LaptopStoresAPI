using LaptopStore.Data.Context;
using LaptopStore.Data.Models;
using LaptopStore.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories
{
    public class OTPRepository : F0GenericRepository<OTP>, IOTPRepository
    {
        public OTPRepository(LaptopStoreDbContext context) : base(context)
        {
        }
        public OTP GetByOTP(string otp)
        {
            return _context.OTPs.Where(x => x.Otpcode == otp).FirstOrDefault();
        }
    }
}
