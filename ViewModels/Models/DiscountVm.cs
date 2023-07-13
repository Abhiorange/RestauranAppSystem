using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class DiscountVm
    {
        public List<SelectListItem>? discount { get; set; }
        public int value { get; set; }
        public int discounttypeid {get; set; }

    }
}