using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class CategoryEditVmApi
    {
        public int Id { get; set; }
        public string categoryname { get; set; }
        public long stocks { get; set; }
        public int isActive { get; set; }
        public string imagesrc { get; set; }

    }
}
