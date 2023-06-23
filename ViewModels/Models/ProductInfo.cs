using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class ProductInfo
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public int unit { get; set; }
        public long unitprice { get; set; }
        public string categoryname { get; set; }
    }
}
