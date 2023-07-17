using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class OrdersVm
    {
        public int orderid { get; set; }
        public long Totalprice { get; set; }
        public int tableno { get; set; }
        public DateTime ordertime { get; set; }
    }
}
