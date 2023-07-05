using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Models;

namespace dataRepository.Interface
{
    public interface IOrdersRepository
    {
        public List<OrdersVm> GetOrdersInfo();
        public List<ProductODVm> GetOrdersInfo(int id);
    }
}
