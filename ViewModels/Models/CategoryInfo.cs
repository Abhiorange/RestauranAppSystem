using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Models
{
    public class CategoryInfo
    {
        public int CategoryId { get; set; }
        public string categoryname { get; set; }
        public int isactive { get; set; }
        public long stocks { get; set; }
    }
}
