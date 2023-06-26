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
        [Required(ErrorMessage = "CompanyId is required")]
        public int companyId { get; set; }
        [Required(ErrorMessage = "Contact is required")]
        public long contact { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Company is required")]
        public List<SelectListItem> Companies { get; set; }
    }
}
