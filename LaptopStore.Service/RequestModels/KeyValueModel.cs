using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class KeyValueModel<T,Y> where T : class where Y : class 
    {
        public T Key { get; set; }
        public Y Value { get; set; }
    }
}
