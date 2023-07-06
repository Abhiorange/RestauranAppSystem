using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class ProductDetail
    {
        public string productname { get; set; }
        public int id { get;set; }
        public long unitprice { get; set; }
        public string image { get; set; }
    }
}
