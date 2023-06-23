using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class ProductAddVm
    {

        [Required(ErrorMessage = "ProductName is required")]
        public string Productname { get; set; }
       
        [Required(ErrorMessage = "Units is required")]
        public string units { get; set; }
        [Required(ErrorMessage = "UnitPrice is required")]
        public int unitprice { get; set; }
      
        public int categoryId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public List<SelectListItem> Categories { get; set; }
    }
}
