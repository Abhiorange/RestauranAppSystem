using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Models;

namespace RestaurantWebApi.Areas.discount.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountRepository _discountrepo;
        private readonly IConfiguration _configuration;
        public string connectionstring = "Server=server=192.168.2.59\\SQL2019;Database=RestaurantPOS;User Id=sa;Password=Tatva@123;Encrypt=False";
        public DiscountsController(IDiscountRepository discountrepo, IConfiguration configuration)
        {
            _discountrepo = discountrepo;
            _configuration = configuration;

        }


        [HttpGet]
        public IActionResult Discounts() //Get the company list
        {
            var discounts = _discountrepo.GetDiscountList();
            return Ok(discounts);
        }
        [HttpPost]
        public IActionResult AddDiscount(DiscountVm model)
        {
            var i = _discountrepo.adddiscount(model);
            if (i >= -1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Not Updated");
            }
        }

        public IActionResult AllDiscountList()
        {
            var users = _discountrepo.AllDiscountList();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public IActionResult DeleteDiscountById(int id)
        {
            var i = _discountrepo.DeleteDiscountById(id);
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