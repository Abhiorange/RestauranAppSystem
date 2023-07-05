using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class CustomerDetailVm
    {
        [Required(ErrorMessage ="Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "CustomerCode is required")]
        public int customerCode { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string email { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public long phone { get; set; }
    }
}
