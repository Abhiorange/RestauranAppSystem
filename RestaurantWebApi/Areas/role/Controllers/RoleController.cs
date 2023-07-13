using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;
using ViewModels.Models;

namespace RestaurantWebApi.Areas.role.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        // "Server=server=192.168.2.59\\SQL2019;Database=itmes;User Id=sa;Password=Tatva@123;Encrypt=False"   
        private readonly IRoleRepository _rolerepo;
        private readonly IConfiguration _configuration;
        public string connectionstring = "Server=server=192.168.2.59\\SQL2019;Database=RestaurantPOS;User Id=sa;Password=Tatva@123;Encrypt=False";
        public RoleController(IRoleRepository rolerepo, IConfiguration configuration)
        {
            _rolerepo = rolerepo;
            _configuration = configuration;

        }

        [HttpPost]
        public IActionResult AddRole(RoleRegisterVm model) //Add Users in database
        {
            var i = _rolerepo.addrole(model);
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
        public IActionResult AllRoles()
        {
            var users = _rolerepo.GetRoleList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult RoleById(int id)
        {
            try
            {
                //RoleById product = new RoleById();
                var model = _rolerepo.RoleById(id);
               
                //product.productname = model.productname;
               
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult EditRole(RoleById roleById)
        {
            try
            {
                var response = _rolerepo.EditRole(roleById);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult DeleteRoleById(int id)
        {
            var i = _rolerepo.deleteRoleById(id);
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