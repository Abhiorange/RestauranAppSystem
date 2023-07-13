using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class RoleRegisterVm
    {
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "description is required")]
        public string description { get; set; }

        public List<RoleInfoVm>? roles;
        
        public RoleById? rolebyid { get; set; }                  



    }
}
