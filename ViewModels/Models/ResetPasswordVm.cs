using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class ResetPasswordVm
    {
        public string email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string password { get; set; }
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Cpassword { get; set; }
    }
}
