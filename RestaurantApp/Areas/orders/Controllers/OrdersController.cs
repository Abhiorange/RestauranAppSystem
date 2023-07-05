using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ViewModels.Models;

namespace RestaurantApp.Areas.orders.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        Uri baseAddress = new Uri("http://localhost:7189/api");

        public OrdersController(ILogger<OrdersController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult OrdersDetails(int orderid=0)
        {
            List<OrdersVm> model = new List<OrdersVm>();
            List<ProductODVm> model1 =new List<ProductODVm>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Orders/GetOrdersInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<OrdersVm>>(userJson);
            }
           
            HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "/Orders/GetDataById" + orderid).Result;
            if (response1.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model1 = JsonConvert.DeserializeObject<List<ProductODVm>>(userJson);
            }
            var tuple = new Tuple<List<OrdersVm>, List<ProductODVm>>(model, model1);
            return View(tuple);
        }
       /* [HttpGet]
        public IActionResult GetDataById(int orderId)
        {
            List<ProductODVm> model = List<ProductODVm>();
           HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Orders/GetDataById"+orderId).Result;
           if (response.IsSuccessStatusCode)
           {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<ProductODVm>>(userJson);
            }
        }*/
    }
}
