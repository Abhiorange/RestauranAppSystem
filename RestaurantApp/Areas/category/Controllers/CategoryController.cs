using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantApp.Areas.admin.Controllers;
using System.Text;
using ViewModels.Models;

namespace RestaurantApp.Areas.category.Controllers
{
   
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        Uri baseAddress = new Uri("http://localhost:7189/api");

        public CategoryController(ILogger<CategoryController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult categories()
        {
            return View("CategoryForm");
        }
        [HttpPost]
        public IActionResult AddCategories([Bind(Prefix = "Item2")]  CategoryVm model)
        {
            try
            {
                var FileName = "";
                using (var ms = new MemoryStream())
                {
                    model.image.CopyToAsync(ms);
                    var imageBytes = ms.ToArray();
                    var base64String = Convert.ToBase64String(imageBytes);
                    FileName = base64String;
                }
                model.imagesrc = FileName;

                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Category/AddCategory", stringContent).Result;

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
            TempData["success"] = "Category in added succesfully";
            return RedirectToAction("AllCategories");
        }

        [HttpGet]
        public IActionResult AllCategories() //rendering the list of users in UI
        {
            List<CategoryInfo> model = new List<CategoryInfo>();
            CategoryVm addVm =new CategoryVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Category/AllCategoryInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<CategoryInfo>>(userJson);
            }
            var tuple = new Tuple<List<CategoryInfo>, CategoryVm>(model, addVm);
            return View(tuple);
         

        }
        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            CategoryEditVm model = new CategoryEditVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Category/GetCategoryById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<CategoryEditVm>(userJson);
            }
            return View("CategoryEditPage", model);
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryEditVm model)
        {
           
            try
            {

                var FileName = "";
                using (var ms = new MemoryStream())
                {
                    model.image.CopyToAsync(ms);
                    var imageBytes = ms.ToArray();
                    var base64String = Convert.ToBase64String(imageBytes);
                    FileName = "data:image/png;base64," + base64String;


                }
                model.imagesrc = FileName;

                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Category/EditCategory", stringContent).Result;

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
            TempData["success"] = "Category in Edited succesfully";
            return RedirectToAction("AllCategories");
        }
        public IActionResult DeleteCategoryById(int id)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Category/DeleteCategoryById/" + id).Result;
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
            TempData["success"] = "Category in Deleted succesfully";
            return RedirectToAction("AllCategories");
        }
    }
}
