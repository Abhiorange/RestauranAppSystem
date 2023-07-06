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
        Uri baseAddress = new Uri("http://localhost:7189/api");

        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        #region
        /*  [HttpGet]
          public IActionResult ProductForm()
          {
              ProductAddVm model = new ProductAddVm();
              HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Product/ProductAdd").Result;

              if (response.IsSuccessStatusCode)
              {
                  var categoryJson = response.Content.ReadAsStringAsync().Result;
                  model.Categories = JsonConvert.DeserializeObject<List<SelectListItem>>(categoryJson);
              }
              return View(model);
          }*/
        #endregion
        [HttpGet]
        public IActionResult AllProducts() //rendering the list of users in UI
        {
            //  List<ProductInfo> model = new List<ProductInfo>();
            ProductInfoEditVm promodel = new ProductInfoEditVm();
            ProductAddVm addVm= new ProductAddVm();
            promodel.productadd = new ProductAddVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Product/AllProductInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                promodel.products = JsonConvert.DeserializeObject<List<ProductInfo>>(userJson);
            }
            HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "/Product/ProductAdd").Result;
            if (response1.IsSuccessStatusCode)
            {
                var categoryJson = response1.Content.ReadAsStringAsync().Result;
                promodel.productadd.Categories = JsonConvert.DeserializeObject<List<SelectListItem>>(categoryJson);
            }
            var tuple = new Tuple<ProductAddVm, ProductInfoEditVm>(addVm,promodel);
            return View(tuple);
            //return View(promodel);

        }
        [HttpPost]
        public IActionResult ProductAdd([Bind(Prefix = "Item1")] ProductAddVm model)//Adding users post method for UserRegister
        {
            var FileName = "";
            using (var ms = new MemoryStream())
            {
                model.image.CopyToAsync(ms);
                var imageBytes = ms.ToArray();
                var base64String = Convert.ToBase64String(imageBytes);
                FileName = base64String;
            }
            PostProductAddVm user = new PostProductAddVm
            {
              Productname= model.Productname,
              categoryId=model.categoryId,
              unitprice=model.unitprice,
              units=model.units,
              imageSrc=FileName,
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
            TempData["success"] = "Product Added Scuccesfully";
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
            var FileName = "";
            using (var ms = new MemoryStream())
            {
                model.image.CopyToAsync(ms);
                var imageBytes = ms.ToArray();
                var base64String = Convert.ToBase64String(imageBytes);
                FileName = "data:image/png;base64,"+ base64String;
            }
            PostProductEditVm product = new PostProductEditVm
            {
                productid = model.productid,
                productname = model.productname,
                unit = model.unit,
                unitprice = model.unitprice,
                categoryid = model.categoryid,
                isactive = model.isactive,
                imageSrc = FileName,
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
            TempData["success"] = "Product Edited Scuccesfully";
            return RedirectToAction("AllProducts");
        }

        public IActionResult DeleteProductById(int id)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Product/DeleteProductById/" + id).Result;
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

    }
}
