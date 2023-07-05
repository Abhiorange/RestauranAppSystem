using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class ProductODVm
    {
        public string productname { get; set; }
        public string image { get; set; }
        public int units { get; set; }
        public long totalprice { get; set; }
        public int unitprice { get; set; }

    }
}
