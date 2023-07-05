using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantWebApi.Areas.orders.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersrepo;
        private readonly IConfiguration _configuration;
        public string connectionstring = "Server=server=192.168.2.59\\SQL2019;Database=RestaurantPOS;User Id=sa;Password=Tatva@123;Encrypt=False";
        public OrdersController(IOrdersRepository ordersrepo, IConfiguration configuration)
        {
            _ordersrepo = ordersrepo;
            _configuration = configuration;

        }
        [HttpGet]
        public IActionResult GetOrdersInfo()
        {
            var details = _ordersrepo.GetOrdersInfo();
            return Ok(details);
        }
        [HttpGet("{id}")]
        public IActionResult GetDataById(int id)
        {
            var details = _ordersrepo.GetOrdersInfo(id);
            return Ok(details);
        }


    }
}
