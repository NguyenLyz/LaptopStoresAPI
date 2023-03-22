using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopStore.Service.RequestModels
{
    public class CommentRequestModel
    {
        public int ProductId { get; set; }
        public string Content { get; set; }
    }
}
