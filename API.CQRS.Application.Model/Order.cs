using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.CQRS.Application.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderDetails { get; set; }
        public bool IsActive { get; set; }
        public DateTime OrderedDate { get; set; }
    }
}
