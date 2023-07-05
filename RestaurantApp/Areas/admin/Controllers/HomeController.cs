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

namespace RestaurantApp.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        Uri baseAddress = new Uri("http://localhost:7189/api");

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }
       
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(UserLoginVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/UserLogin", stringContent).Result;
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
                            return View("Dashboard");
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
            return RedirectToAction("AllUsers");
        }
        [HttpGet]
        public IActionResult UserLogin() //User Login GET
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserLogin(UserLoginVm model) //For User Login
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/UserLogin", stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        var content = response.Content.ReadAsStringAsync().Result;

                        if (content == "false")
                        {
                            TempData["error"]="Invalid Credential";
                            return View("UserLogin");
                        }
                        else
                        {
                            TempData["success"] = "Logged in done succesfully";
                            return RedirectToAction("Dashboard");
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
            return RedirectToAction("AllUsers");
        }
        //All Users List
        #region
        [HttpGet]
        public IActionResult AllUsers() //rendering the list of users in UI
        {
            List<Userinfo> model = new List<Userinfo>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/AllUsersInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<Userinfo>>(userJson);
            }
            return View(model);

        }
        #endregion
        //for user register
        #region
        [HttpGet]
        public IActionResult UserRegister() //For user register get
        {
            UserRegisterVm model = new UserRegisterVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/UserRegister").Result;

            if (response.IsSuccessStatusCode)
            {
                var companiesJson = response.Content.ReadAsStringAsync().Result;
                model.Companies = JsonConvert.DeserializeObject<List<SelectListItem>>(companiesJson);
            }
            return View(model);
        }
        #endregion
        //Adding users post method for UserRegister
        #region
        [HttpPost]
        public IActionResult UserAdd(UserRegisterVm model)
        {
            PostUserRegisterVm user = new PostUserRegisterVm
            {
                name = model.name,
                companyId = model.companyId,
                email = model.email,
                contact = model.contact,
                userCode = model.userCode,
                password = model.password
            };
            try
            {
                string serializedData = JsonConvert.SerializeObject(user);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/AddUser", stringContent).Result;

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
            return RedirectToAction("UserLogin");
        }
        #endregion

        [HttpGet]
        public IActionResult AllCompanies() //rendering the list of users in UI
        {
            List<CompanyInfo> model = new List<CompanyInfo>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/AllCompanyInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<CompanyInfo>>(userJson);
            }
            return View(model);

        }
        //For company login GET
        public IActionResult CompanyLogin()
        {
           
            return View("CompanyLogin");
        }
        #region
        [HttpPost]
        public IActionResult Login(CompanyLoginVm model)//for company login
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
                            return View("CompanyLogin");
                        }
                        else
                        {
                            TempData["success"] = "Logged in done succesfully";
                            return RedirectToAction("AllCompanies");
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
        #endregion//for company login
        [HttpGet]
        public IActionResult CompanyRegister()
        {
            return View();
           
        }

        [HttpPost]
        public IActionResult Register(CompanyRegisterVm model) //For Company Register
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/Register", stringContent).Result;
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
            TempData["success"] = "You are registered successfully";
            return RedirectToAction("CompanyLogin");
        }





        [HttpGet]
        public IActionResult Register() //For Company Register
        {
            return View();
        }
       
        
       

       
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            UserEditVm model = new UserEditVm();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/GetUserById/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<UserEditVm>(userJson);
            }
            return View("UserEditPage", model);
        }
        [HttpPost]
        public IActionResult GetUserById(UserEditVm model)
        {
            PostUserEditVm user = new PostUserEditVm
            {
                UserId = model.UserId,
                name = model.name,
                password = model.password,
                companyId = model.companyId,
                email = model.email,
                contact = model.contact,
                isactive=model.isactive
            };
            try
            {
                string serializedData = JsonConvert.SerializeObject(user);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/EditUser", stringContent).Result;

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
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Admin/DeleteUserById/"+id).Result;
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
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        private string GenerateOTP()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }
     

        [HttpPost]
        public ActionResult ForgetPassword(string email)
        {
            string OTP = GenerateOTP();
            ForgetPasswordVm model = new ForgetPasswordVm();
            model.email = email;
                model.otp = OTP;

                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/ForgetPassword", stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        var content = response.Content.ReadAsStringAsync().Result;

                        if (content == "false")
                        {
                            TempData["error"] = "Invalid Credential";
                            return View("ForgetPassword");
                        }
                        else
                        {
                        var fromAddress = new MailAddress("pankru2002@gmail.com", "Krunamissionanjabi");
                        var toAddress = new MailAddress(model.email);
                        var subject = "Password reset request";
                        var body = $"Hi,<br /><br />this is your one time password:<br /><br /><a href='{OTP}'>{OTP}</a>";
                        var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true
                        };
                        var smtpClient = new SmtpClient("smtp.gmail.com", 587)
                        {
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential("pankru2002@gmail.com", "iumtuyfqrdpwsfcq"),
                            EnableSsl = true
                        };
                        smtpClient.Send(message);
                        TempData["success"] = "Logged in done succesfully";
                            return View();
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
            
            return View();
        }
        [HttpPost]
        public IActionResult OtpCompare(ForgetPasswordVm model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string serializedData = JsonConvert.SerializeObject(model);
                    StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/OtpCompare", stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        var content = response.Content.ReadAsStringAsync().Result;

                        if (content == "false")
                        {
                            TempData["error"] = "Invalid Credential";
                            return View("ForgetPassword");
                        }
                        else
                        {
                            TempData["success"] = "Logged in done succesfully";
                            return RedirectToAction("ResetPassword", new { email = model.email });
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
            return RedirectToAction("ResetPassword",model.email);
        }

        public ActionResult ResetPassword(string email)
        {
            ResetPasswordVm resetPassword = new ResetPasswordVm
            {
                email = email
            };

            return View(resetPassword);
        }

        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordVm model)
        {
            try
            {
                string serializedData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(serializedData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Admin/ResetPassword", stringContent).Result;
                if (response.IsSuccessStatusCode)
                {

                    var content = response.Content.ReadAsStringAsync().Result;

                    if (content == "false")
                    {
                        TempData["error"] = "Invalid Credential";
                        return View("ForgetPassword");
                    }
                    else
                    {
                        TempData["success"] = "Logged in done succesfully";
                        return View("Index");
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

            return View();

           
        }



        //    public class AccountController : Controller
        //{
        //    private readonly string connectionString = "YourConnectionString"; // Replace with your actual connection string

        //    // GET: Account/ForgotPassword
        //    public ActionResult ForgotPassword()
        //    {
        //        return View();
        //    }

        //    // POST: Account/ForgotPassword
        //    [HttpPost]
        //    public ActionResult ForgotPassword(string email)
        //    {
        //        // Generate a random 4-digit OTP
        //        string otp = GenerateOTP();

        //        // Save the OTP and email in the database
        //        SaveOTPInDatabase(email, otp);

        //        // Send the OTP to the user's email
        //        SendOTPByEmail(email, otp);

        //        return RedirectToAction("ResetPassword");
        //    }

        //    // GET: Account/ResetPassword
        //    public ActionResult ResetPassword()
        //    {
        //        return View();
        //    }

        //    // POST: Account/ResetPassword
        //    [HttpPost]
        //    public ActionResult ResetPassword(string email, string otp, string newPassword)
        //    {
        //        // Verify the OTP entered by the user
        //        if (VerifyOTP(email, otp))
        //        {
        //            // Update the user's password in the database
        //            UpdatePasswordInDatabase(email, newPassword);

        //            // Display a success message or redirect to a login page
        //            ViewBag.Message = "Password updated successfully.";
        //            return View("Success");
        //        }
        //        else
        //        {
        //            ViewBag.ErrorMessage = "Invalid OTP entered.";
        //            return View("ResetPassword");
        //        }
        //    }

        //    // Helper method to generate a random 4-digit OTP
        //    private string GenerateOTP()
        //    {
        //        Random random = new Random();
        //        return random.Next(1000, 9999).ToString();
        //    }

        //    // Helper method to save the OTP and email in the database
        //    private void SaveOTPInDatabase(string email, string otp)
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            SqlCommand command = new SqlCommand("YourStoredProcedureName", connection);
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@Email", email);
        //            command.Parameters.AddWithValue("@OTP", otp);
        //            command.ExecuteNonQuery();
        //        }
        //    }

        //    // Helper method to send the OTP to the user's email
        //    private void SendOTPByEmail(string email, string otp)
        //    {
        //        MailMessage mailMessage = new MailMessage();
        //        mailMessage.From = new MailAddress("your-email@example.com"); // Replace with your email address
        //        mailMessage.To.Add(email);
        //        mailMessage.Subject = "Reset Password OTP";
        //        mailMessage.Body = $"Your OTP is: {otp}";

        //        SmtpClient smtpClient = new SmtpClient("smtp.example.com"); // Replace with your SMTP server address
        //        smtpClient.Port = 587;
        //        smtpClient.Credentials = new System.Net.NetworkCredential("your-email@example.com", "your-password"); // Replace with your email credentials
        //        smtpClient.EnableSsl = true;
        //        smtpClient.Send(mailMessage);
        //    }

        //    // Helper method to verify the OTP from the database
        //    private bool VerifyOTP(string email, string otp)
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            SqlCommand command = new SqlCommand("YourStoredProcedureName", connection);
        //            command.CommandType










    }
        }