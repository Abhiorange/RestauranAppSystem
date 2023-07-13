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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using Azure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RestaurantApp.Areas.role.Controllers
{
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        Uri baseAddress = new Uri("http://localhost:7189/api");

        public RoleController(ILogger<RoleController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [HttpGet]
        public IActionResult RoleRegister()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RoleRegister(RoleRegisterVm model)
        {
            try
            {
                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Role/AddRole", stringContent).Result;

            }
            catch (HttpRequestException ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("AllRoles");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("AllRoles");
            }
            TempData["success"] = "Logged in done succesfully";
            return RedirectToAction("AllRoles");
        }


        [HttpGet]
        public IActionResult AllRoles() //rendering the list of users in UI
        {
            //  List<ProductInfo> model = new List<ProductInfo>();
            RoleRegisterVm model = new RoleRegisterVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/AllRoles").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model.roles = JsonConvert.DeserializeObject<List<RoleInfoVm>>(userJson);
            }

            return View(model);
            //return View(promodel);

        }


        [HttpGet]
        public IActionResult GetRoleById(int id) //rendering the list of users in UI
        {
            RoleById model = new RoleById();
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/RoleById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                   
                    var userJson = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<RoleById>(userJson);
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
            TempData["success"] = "Product Edited Scuccesfully";
            return Json(model);
        }

        [HttpPost]
        public IActionResult RoleEdit(RoleRegisterVm model)
        {
            try
            {
                string serializedData = JsonConvert.SerializeObject(model.rolebyid);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Role/EditRole", stringContent).Result;

            }
            catch (HttpRequestException ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("AllRoles");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("AllRoles");
            }
            TempData["success"] = "Logged in done succesfully";
            return RedirectToAction("AllRoles");
        }

    
        public IActionResult DeleteRoleById(int id)
        {
            try
            {
          
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Role/DeleteRoleById/" + id).Result;

            }
            catch (HttpRequestException ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("AllRoles");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "Error: " + ex.Message;
                return View("AllRoles");
            }
            TempData["success"] = "Logged in done succesfully";
            return RedirectToAction("AllRoles");
        }
    }
}