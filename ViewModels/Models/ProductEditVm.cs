using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class ProductEditVm
    {
        public int productid { get; set; }
        public string productname { get; set; }
        public int unit { get; set; }
        public long unitprice { get; set; }
        public int isactive { get; set; }
        public int categoryid { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
