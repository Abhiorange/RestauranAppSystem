using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Security.Cryptography;
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
        private readonly Random random = new Random();

        public ServiceController(ILogger<ServiceController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult ServicesPage(int id,int orderid=0,int tableid=0)
        {
            List<CategoryDetail> model = new List<CategoryDetail>();
            List<ProductDetail> model1 = new List<ProductDetail>();
            List<ItemsInfo> item = new List<ItemsInfo>();
            List<AllItemsInfo> products = new List<AllItemsInfo>();
            CustomerDetailVm model2 = new CustomerDetailVm();
            List<TableDetailVm> table = new List<TableDetailVm>();
          
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Service/GetCategoryNames").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<CategoryDetail>>(userJson);
            }

            HttpResponseMessage response4 = _client.GetAsync(_client.BaseAddress + "/Service/GetTableInfo").Result;
            if (response4.IsSuccessStatusCode)
            {
                var tableJson = response4.Content.ReadAsStringAsync().Result;
                table = JsonConvert.DeserializeObject<List<TableDetailVm>>(tableJson);
            }


            HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "/Service/GetProductsById/" + id).Result;
            if (response1.IsSuccessStatusCode)
            {
                var prod = response1.Content.ReadAsStringAsync().Result;
                model1 = JsonConvert.DeserializeObject<List<ProductDetail>>(prod);
            }


            if(tableid!=0)
            {
                HttpResponseMessage response5 = _client.GetAsync(_client.BaseAddress + "/Service/GetOrderId/" + tableid).Result;
                if (response5.IsSuccessStatusCode)
                {
                    var content = response5.Content.ReadAsStringAsync().Result;
                    orderid = Convert.ToInt32(content);

                }

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
            var tuple = new Tuple<List<CategoryDetail>,List<ProductDetail>,List<ItemsInfo>,List<AllItemsInfo>,CustomerDetailVm,List<TableDetailVm>>(model,model1,item,products,model2,table);
            return View(tuple);

        }
        public IActionResult PaymentPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CustomerAdd([Bind(Prefix = "Item5")] CustomerDetailVm model)
        {
            PaymentVm payment = new PaymentVm();
            int customerId = 0;
            try
            {
                int randomNumber = random.Next();
                model.customerCode = randomNumber;
                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Service/AddCustomer", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    customerId = Convert.ToInt32(content);
                }


                payment.customerId=customerId;
                payment.orderid = (int)HttpContext.Session.GetInt32("ordersid");
                string serializedData1 = JsonConvert.SerializeObject(payment);
                StringContent stringContent1 = new StringContent(serializedData1, Encoding.UTF8, "application/json");
                HttpResponseMessage response1 = _client.PostAsync(_client.BaseAddress + "/Service/Payment/" ,stringContent1).Result;
                if (response1.IsSuccessStatusCode)
                {
                    HttpContext.Session.Remove("ordersid");
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
        public IActionResult AddItems([FromBody] ItemsDetailVm model)
        {
            PostItemsVm item = new PostItemsVm();
            int orderId = 0;
                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Service/AddOrder", stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        orderId = Convert.ToInt32(content);
                    }
                }
                catch (Exception ex)
                {
                    ViewData["ErrorMessage"] = "Error: " + ex.Message;
                    return View("Index");
                }

            item.ordersid = orderId;
            try
            {
                item.productid = model.productid;
                item.itemunit = model.itemunit;
                string serializedData = JsonConvert.SerializeObject(item);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response1 = _client.PostAsync(_client.BaseAddress + "/Service/AddItems", stringContent).Result;
                if (response1.IsSuccessStatusCode)
                {
                    return RedirectToAction("ServicesPage", new { orderid = orderId, tableid = 0 });
                  //  return RedirectToAction("ServicesPage");
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
                    return RedirectToAction("ServicesPage", new { orderid = model.orderid, tableid = 0 });
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("ServicesPage", new { orderid = model.orderid, tableid = 0 });
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
                    return RedirectToAction("ServicesPage", new { orderid = model.orderid, tableid = 0 });
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("ServicesPage", new { orderid = model.orderid, tableid = 0 });
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
                    return RedirectToAction("ServicesPage", new { orderid = model.orderid, tableid = 0 });
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("ServicesPage", new { orderid = model.orderid, tableid = 0 });
        }
        [HttpPost]
        public IActionResult PayCash([FromBody] TablenoVm model)
        {
            try
            {
                HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "/Service/GetOrderId/" + model.tableid).Result;
                if (response1.IsSuccessStatusCode)
                {
                    var content = response1.Content.ReadAsStringAsync().Result;
                    model.orderid = Convert.ToInt32(content);

                }

                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Service/PayCash", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ServicesPage", new { orderid = 0, tableid = 0 });
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("ServicesPage", new { orderid = 0, tableid = 0 });
        }

        [HttpPost]
        public IActionResult BillItems([FromBody] TablenoVm model)
        {
            try
            {
                HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "/Service/GetOrderId/" + model.tableid).Result;
                if (response1.IsSuccessStatusCode)
                {
                    var content = response1.Content.ReadAsStringAsync().Result;
                    model.orderid = Convert.ToInt32(content);

                }

                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Service/BillItems", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("ServicesPage", new { orderid = model.orderid, tableid = 0 });
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("ServicesPage", new { orderid = model.orderid, tableid = 0 });
        }
    }
}
