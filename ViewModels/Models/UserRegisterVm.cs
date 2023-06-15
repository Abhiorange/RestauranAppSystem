using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class UserRegisterVm
    {
        public string name { get; set; }
        public string userCode { get; set; }

        public string email { get; set; }

        public long contact { get; set; }
        public string password { get; set; }
    }
}
