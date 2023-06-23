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
        Uri baseAddress = new Uri("https://localhost:7189/api");

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
            return PartialView("_categoryForms");
        }
        [HttpPost]
        public IActionResult AddCategories(CategoryVm model)
        {
            try
            {
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
            return RedirectToAction("AllCategories");
        }

        [HttpGet]
        public IActionResult AllCategories() //rendering the list of users in UI
        {
            List<CategoryInfo> model = new List<CategoryInfo>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Category/AllCategoryInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<CategoryInfo>>(userJson);
            }
            return View(model);

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
        public IActionResult GetCategoryById(CategoryEditVm model)
        {
           
            try
            {
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
            return RedirectToAction("AllCategories");
        }
    }
}
