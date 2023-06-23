using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using ViewModels.Models;

namespace RestaurantApp.Areas.product.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        Uri baseAddress = new Uri("https://localhost:7189/api");

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult ProductAddForm()//for product add
        {
            ProductAddVm model = new ProductAddVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Product/ProductAdd").Result;

            if (response.IsSuccessStatusCode)
            {
                var categoryJson = response.Content.ReadAsStringAsync().Result;
                model.Categories = JsonConvert.DeserializeObject<List<SelectListItem>>(categoryJson);
            }
            return PartialView("_productForm", model);
        }
        [HttpGet]
        public IActionResult AllProducts() //rendering the list of users in UI
        {
            List<ProductInfo> model = new List<ProductInfo>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Product/AllProductInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<ProductInfo>>(userJson);
            }
            return View(model);

        }
        [HttpPost]
        public IActionResult ProductAdd(ProductAddVm model)//Adding users post method for UserRegister
        {
            PostProductAddVm user = new PostProductAddVm
            {
              Productname= model.Productname,
              categoryId=model.categoryId,
              unitprice=model.unitprice,
              units=model.units,
            };
            try
            {
                string serializedData = JsonConvert.SerializeObject(user);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Product/AddProduct", stringContent).Result;

            }
            catch (HttpRequestException ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }

            return RedirectToAction("AllProducts");
        }
        [HttpGet]
        public IActionResult GetProductById(int id)
        {
            ProductEditVm model = new ProductEditVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Product/GetProductById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductEditVm>(userJson);
            }
            return View("ProductEditPage", model);
        }
        [HttpPost]
        public IActionResult GetProductById(ProductEditVm model)
        {
            PostProductEditVm product = new PostProductEditVm
            {
                productid = model.productid,
                productname = model.productname,
                unit = model.unit,
                unitprice = model.unitprice,
                categoryid = model.categoryid,
                isactive = model.isactive
            };
            try
            {
                string serializedData = JsonConvert.SerializeObject(product);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Product/EditProduct", stringContent).Result;

            }
            catch (HttpRequestException ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("Index");
            }
            return RedirectToAction("AllUsers");
        }

    }
}
