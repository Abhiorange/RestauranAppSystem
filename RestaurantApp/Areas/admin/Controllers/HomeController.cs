using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantApp.Models;
using System.Configuration;
using System.Diagnostics;
using System.Security.Claims;
using ViewModels.Models;
using System.Text;

namespace RestaurantApp.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        Uri baseAddress = new Uri("https://localhost:7189/api");

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(CompanyLoginVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                 
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/Login", stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                  
                        var content = response.Content.ReadAsStringAsync().Result;

                        if (content == "false")
                        {
                            TempData["error"] = "Invalid Credential";
                            return View("Index");
                        }
                        else
                        {
                         
                          
                                TempData["success"] = "Logged in done succesfully";
                                return Redirect("Register");
                            
                        }
                    }
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
            }
            return View("Index");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(CompanyRegisterVm model)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}