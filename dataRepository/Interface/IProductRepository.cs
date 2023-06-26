using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;

namespace dataRepository.Interface
{
    public interface IProductRepository
    {
        public List<SelectListItem> GetProductList();
        public int AddProduct(PostProductAddVm model);
        public List<ProductInfo> GetProductsList();
        public ProductEditVm GetProductById(int id);
        public int EditProduct(PostProductEditVm model);
        public int deleteProductById(int id);
    }
}
