using LaptopStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.Repositories.Interfaces
{
    public interface IOTPRepository : IF0GenericRepository<OTP>
    {
        OTP GetByOTP(string otp);
        OTP GetByPhone(string phone);
        OTP GetByEmail(string email);
    }
}
