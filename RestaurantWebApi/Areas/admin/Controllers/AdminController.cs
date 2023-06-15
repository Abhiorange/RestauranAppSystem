using Microsoft.AspNetCore.Mvc;
using ViewModels.Models;

namespace RestaurantWebApi.Areas.admin.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AdminController : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(CompanyLoginVm model)
        {
            return Ok(); 
        }
       
    }
}
