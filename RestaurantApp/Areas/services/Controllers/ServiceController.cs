using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
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
        public IActionResult ServicesPage(int id)
        {
            List<CategoryDetail> model = new List<CategoryDetail>();
            List<ProductDetail> model1 = new List<ProductDetail>();
            List<ItemsInfo> item = new List<ItemsInfo>();
            List<AllItemsInfo> products = new List<AllItemsInfo>();
            CustomerDetailVm model2 = new CustomerDetailVm();
            int orderid = 0;
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
            if (HttpContext.Session.GetInt32("ordersid") != null)
            {
                orderid = (int)HttpContext.Session.GetInt32("ordersid");
            }
            HttpResponseMessage response2 = _client.GetAsync(_client.BaseAddress + "/Service/GetItems/" + orderid).Result;
            if (response2.IsSuccessStatusCode)
            {
                var prod = response2.Content.ReadAsStringAsync().Result;
                item = JsonConvert.DeserializeObject<List<ItemsInfo>>(prod);
            }
            HttpResponseMessage response3 = _client.GetAsync(_client.BaseAddress + "/Service/GetAllProductsNames/" + orderid).Result;
            if (response3.IsSuccessStatusCode)
            {
                var prod1 = response3.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<AllItemsInfo>>(prod1);
            }
            var tuple = new Tuple<List<CategoryDetail>,List<ProductDetail>,List<ItemsInfo>,List<AllItemsInfo>,CustomerDetailVm>(model,model1,item,products,model2);
            return View(tuple);

        }
        public IActionResult PaymentPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerAdd([FromBody] CustomerDetailVm model)
        {
            try
            {
                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Service/AddCustomer", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    int customerId = Convert.ToInt32(content);
                    HttpContext.Session.SetInt32("customerId", customerId);
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            TempData["success"] = "Product Added Scuccesfully";
           // return RedirectToAction("AllProducts");
            return RedirectToAction("ServicesPage");

        }
        [HttpPost]
        public IActionResult AddItems([FromBody] ItemsDetailVm model)
        {
            PostItemsVm item = new PostItemsVm();
            if (HttpContext.Session.GetInt32("ordersid") == null)
            {
                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Service/AddOrder", stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        int orderId = Convert.ToInt32(content);
                        HttpContext.Session.SetInt32("ordersid", orderId);
                    }
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Error: " + ex.Message;
                    return View("Index");
                }
            }
            item.ordersid = (int)HttpContext.Session.GetInt32("ordersid");
            try
            {
                item.productid = model.productid;
                item.itemunit = model.itemunit;
                string serializedData = JsonConvert.SerializeObject(item);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response1 = _client.PostAsync(_client.BaseAddress + "/Service/AddItems", stringContent).Result;
                if (response1.IsSuccessStatusCode)
                {
                    return RedirectToAction("ServicesPage");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("ServicesPage");
        }

        public IActionResult IncreItems([FromBody] Increunitvm model)
        {
            try
            {
                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Service/IncreItems", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ServicesPage");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("ServicesPage");
        }
        public IActionResult DecreItems([FromBody] Increunitvm model)
        {
            try
            {
                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Service/DecreItems", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ServicesPage");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("ServicesPage");
        }
        [HttpPost]
        public IActionResult DeleteItems([FromBody] DeleteItemVm model)
        {
            try
            {
                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Service/DeleteItems", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ServicesPage");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("ServicesPage");
        }


        [HttpPost]
        public IActionResult BillItems([FromBody] TablenoVm model)
        {
            try
            {
                int orderId = (int)HttpContext.Session.GetInt32("ordersid");
                model.orderid = orderId;
                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Service/BillItems", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ServicesPage");
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("ServicesPage");
        }
    }
}
