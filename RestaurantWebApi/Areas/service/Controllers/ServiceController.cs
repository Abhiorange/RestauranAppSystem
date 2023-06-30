using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Models;

namespace RestaurantWebApi.Areas.service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _servicerepo;
        private readonly IConfiguration _configuration;
        public string connectionstring = "Server=PCA59\\SQL2019;Database=RestaurantPOS;User Id=sa;Password=Tatva@123;Trusted_Connection=True;Encrypt=False";
        public ServiceController(IServiceRepository servicerepo, IConfiguration configuration)
        {
            _servicerepo = servicerepo;
            _configuration = configuration;

        }
        [HttpGet]
        public IActionResult GetCategoryNames()
        {
            var names = _servicerepo.GetCategoryNames();
            return Ok(names);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductsById(int id)
        {
            try
            {
                var names = _servicerepo.GetProductsById(id);
                return Ok(names);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetItems(int id)
        {
            try
            {
                var names = _servicerepo.GetItems(id);
                return Ok(names);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerDetailVm model) //Add Users in database
        {
            var customerId = _servicerepo.AddCustomer(model);
            return Ok(customerId);
           
        }
        [HttpPost]
        public IActionResult AddOrder(ItemsDetailVm model)
        {
            var i = _servicerepo.AddOrder(model);
            if (i > 0)
            {
                return Ok(i);
            }
            else
            {
                return BadRequest("Wrong credential");
            }
        }

        [HttpPost]
        public IActionResult AddItems(PostItemsVm model)
        {
            var i = _servicerepo.AddItem(model);
            if (i > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Wrong credential");
            }
        }
    }
}
