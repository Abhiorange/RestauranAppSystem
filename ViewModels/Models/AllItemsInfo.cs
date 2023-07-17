using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class AllItemsInfo
    {
        public string itemname { get; set; }
        public int units { get; set; }
        public long totalprice { get; set; }
        public long MainPrice { get; set; }
        public int tablenumber { get; set; }
        public int dicountflag { get; set; }
        public int discountvalue { get; set; }
    }
}
