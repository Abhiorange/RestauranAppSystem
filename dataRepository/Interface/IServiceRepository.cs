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
        public int IncreItems(Increunitvm model);
        public int DecreItems(Increunitvm model);
        public int BillItems(TablenoVm model);
        public List<AllItemsInfo> GetAllProductsNames(int orderid);
        public int DeleteItems(DeleteItemVm model);

    }
}
