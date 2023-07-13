using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class UserRegisterVm
    {
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "userCode is required")]
        public string userCode { get; set; }    
        [Required(ErrorMessage = "Email is required")]

        public string email { get; set; }
        //[Required(ErrorMessage = "CompanyId is required")]
        //public int companyId { get; set; }
        [Required(ErrorMessage = "Role is required")]

        public int roleId { get; set; }
        [Required(ErrorMessage = "Contact is required")]
        public long contact { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }

        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string cpassword { get; set; }
        [Required(ErrorMessage = "Company is required")]
        //public List<SelectListItem> Companies { get; set; }
        public List<SelectListItem> roles { get; set; }
    }
}
