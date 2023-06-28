using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ViewModels.Models;

namespace RestaurantApp.Areas.services.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        Uri baseAddress = new Uri("http://localhost:7189/api");

        public ServiceController(ILogger<ServiceController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult ServicesPage(int id = 2)
        {

            List<CategoryDetail> model = new List<CategoryDetail>();
            List<ProductDetail> model1 = new List<ProductDetail>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Service/GetCategoryNames").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<CategoryDetail>>(userJson);
            }
            HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "/Service/GetProductsById/" + id).Result;
            if (response1.IsSuccessStatusCode)
            {
                var prod = response1.Content.ReadAsStringAsync().Result;
                model1 = JsonConvert.DeserializeObject<List<ProductDetail>>(prod);
            }
            var tuple = new Tuple<List<CategoryDetail>, List<ProductDetail>>(model, model1);
            return View(tuple);

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
