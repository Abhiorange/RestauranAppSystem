using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;

namespace dataRepository.Interface
{
    public interface IDiscountRepository
    {
        public List<SelectListItem> GetDiscountList();

        public int adddiscount(DiscountVm model);

    }
}
