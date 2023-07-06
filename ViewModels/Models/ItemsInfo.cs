using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class ItemsInfo
    {
        public int orderdetailid { get; set; }
        public int orderid { get; set; }
        public string itemname { get; set; }
        public long totalprice { get; set; }
        public int units { get; set; }
        public int productid { get; set; }
        public string image { get; set; }
    }
}
