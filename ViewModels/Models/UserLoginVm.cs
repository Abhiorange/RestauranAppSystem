using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class UserLoginVm
    {
        [Required(ErrorMessage ="Password is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "email is required")]
        public string email { get; set; }
    }
}
