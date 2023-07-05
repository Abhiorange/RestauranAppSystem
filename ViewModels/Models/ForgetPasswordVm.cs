using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class ForgetPasswordVm
    {
        [Required(ErrorMessage = "email is required")]
        public string email { get; set; }
        public string? otp { get; set; }
    }
}
