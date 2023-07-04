using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using ViewModels.Models;

namespace RestaurantWebApi.Areas.admin.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        // "Server=server=192.168.2.59\\SQL2019;Database=itmes;User Id=sa;Password=Tatva@123;Encrypt=False"   
        private readonly ICompanyRepository _companyrepo;
        private readonly IConfiguration _configuration;
        public string connectionstring = "Server=server=192.168.2.59\\SQL2019;Database=RestaurantPOS;User Id=sa;Password=Tatva@123;Encrypt=False";
        public AdminController(ICompanyRepository companyrepo, IConfiguration configuration)
        {
            _companyrepo = companyrepo;
            _configuration = configuration;

        }

        [HttpPost]
        public IActionResult Login(CompanyLoginVm model)//company login
        {
            var userId = _companyrepo.loginrepo(model);
            if (userId != 0)
            {
                return Ok();
            }
            else
            {
                return Content("false");
            }
          
        }


        [HttpPost]
        public IActionResult Register(CompanyRegisterVm model)//company register
        {
            var i = _companyrepo.registerrepo(model);
            if(i>0)
            {
                return Ok();
            }
            else
            {
                return Content("false");
            }
           

        }
        [HttpPost]
        public IActionResult AddUser(PostUserRegisterVm model) //Add Users in database
        {
            var i = _companyrepo.adduser(model);
            if(i>0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Not Updated");
            }
        }
        [HttpPost]        public IActionResult EditUser(PostUserEditVm model)
        {
            var i = _companyrepo.EditUser(model);
            if (i > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Not Updated");
            }
        }
        [HttpGet]
        public IActionResult UserRegister() //Get the company list
        {
            var companies = _companyrepo.GetCompanyList();
            return Ok(companies);
        }
        [HttpGet]
        public IActionResult AllUsersInfo()
        {
            var users = _companyrepo.GetUsersList();
            return Ok(users);
        }
        [HttpGet]
        public IActionResult AllCompanyInfo()
        {
            var companies = _companyrepo.GetCompaniesList();
            return Ok(companies);
        }
        [HttpPost]
        public IActionResult UserLogin(UserLoginVm model) //User is Logged in
        {
            var userId = _companyrepo.userloginrepo(model);
            if (userId != 0)
            {
                return Ok();
            }
            else
            {
                return Content("false");
            }
        }
       
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                UserEditVm user = new UserEditVm();
                var model = _companyrepo.GetUserById(id);
                var companies = _companyrepo.GetCompanyList();
                user.name = model.name;
                user.email = model.email;
                user.companyId = model.companyId;
                user.contact = model.contact;
                user.password = model.password;
                user.Companies = companies;
                user.UserId = id;
                user.isactive = model.isactive;
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetProductsById(int id)
        {
            try
            {
                var names = _companyrepo.GetProductsById(id);
                return Ok(names);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            var i = _companyrepo.deleteUserById(id);
            if (i > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Not Updated");
            }
        }

      


    }
}
/*using (SqlCommand command = new SqlCommand("GetCompanyList", connection))
{
    command.CommandType = CommandType.StoredProcedure;

    using (SqlDataReader reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            int companyId = (int)reader["CompanyId"];
            string companyName = (string)reader["CompanyName"];
            viewModel.Companies.Add(new SelectListItem { Value = companyId.ToString(), Text = companyName });
        }
    }
}*/