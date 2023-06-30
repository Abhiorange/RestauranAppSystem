using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;

namespace dataRepository.Interface
{
    public interface IServiceRepository
    {
        public List<ProductDetail> GetProductsById(int id);
        public List<CategoryDetail> GetCategoryNames();
        public int AddCustomer(CustomerDetailVm model);
        public int AddOrder(ItemsDetailVm model);
        public int AddItem(PostItemsVm model);
        public List<ItemsInfo> GetItems(int id);
    }
}
