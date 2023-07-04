using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class PostProductAddVm
    {
        public string Productname { get; set; }
        public string units { get; set; }
        public int unitprice { get; set; }
        public int categoryId { get; set; }
        public string imageSrc { get; set; }
    }
}
