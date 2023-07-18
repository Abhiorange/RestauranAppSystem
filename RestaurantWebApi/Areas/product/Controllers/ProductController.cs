using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Models;

namespace RestaurantWebApi.Areas.product.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productrepo;
        private readonly IConfiguration _configuration;
        public string connectionstring = "Server=server=192.168.2.59\\SQL2019;Database=RestaurantPOS;User Id=sa;Password=Tatva@123;Encrypt=False";
        public ProductController(IProductRepository productrepo, IConfiguration configuration)
        {
            _productrepo = productrepo;
            _configuration = configuration;

        }
        [HttpGet]
        public IActionResult ProductAdd() //Get the company list
        {
            var companies = _productrepo.GetProductList();
            return Ok(companies);
        }

        [HttpPost]
        public IActionResult AddProduct(PostProductAddVm model) //Add Users in database
        {
            var i = _productrepo.AddProduct(model);
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
        public IActionResult AllProductInfo()
        {
            var users = _productrepo.GetProductsList();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                ProductEditVm product = new ProductEditVm();
                var model = _productrepo.GetProductById(id);
                var categories = _productrepo.GetProductList();
                product.productname=model.productname;
                product.unitprice=model.unitprice;
                product.unit=model.unit;
                product.isactive=model.isactive;
                product.categoryid=model.categoryid;
                product.Categories = categories;
                product.productid = id;
                product.imageSrc = model.imageSrc;
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut]
        public IActionResult EditProduct(PostProductEditVm model)
        {
            var i = _productrepo.EditProduct(model);
            if (i >= -1)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Not Updated");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProductById(int id)
        {
            var i = _productrepo.deleteProductById(id);
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
