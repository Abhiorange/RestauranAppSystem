using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestaurantApp.Areas.admin.Controllers;
using System.Text;
using ViewModels.Models;

namespace RestaurantApp.Areas.discount.Controllers
{
    public class DiscountController : Controller
    {
        private readonly ILogger<DiscountController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        Uri baseAddress = new Uri("http://localhost:7189/api");

        public DiscountController(ILogger<DiscountController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }
        [HttpGet]
        public IActionResult Discount()
        {
            DiscountVm model = new DiscountVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Discounts/Discounts").Result;

            if (response.IsSuccessStatusCode)
            {
                var rolesJson = response.Content.ReadAsStringAsync().Result;
                model.discount = JsonConvert.DeserializeObject<List<SelectListItem>>(rolesJson);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AddDiscount(DiscountVm model)
        {
            //PostUserRegisterVm user = new PostUserRegisterVm
            //{
            //    name = model.name,
            //    //companyId = model.companyId,
            //    roleId = model.roleId,
            //    email = model.email,
            //    contact = model.contact,
            //    userCode = model.userCode,
            //    password = model.password
            //};
            try
            {
                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Discounts/AddDiscount", stringContent).Result;

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
            TempData["success"] = "Logged in done succesfully";
            return RedirectToAction("Discount");
        }
    }
}
