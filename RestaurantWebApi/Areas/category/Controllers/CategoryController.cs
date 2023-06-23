using dataRepository.Interface;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Models;

namespace RestaurantWebApi.Areas.category.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryrepo;
        private readonly IConfiguration _configuration;
        public string connectionstring = "Server=PCA59\\SQL2019;Database=RestaurantPOS;User Id=sa;Password=Tatva@123;Trusted_Connection=True;Encrypt=False";
        public CategoryController(ICategoryRepository categoryrepo, IConfiguration configuration)
        {
            _categoryrepo = categoryrepo;
            _configuration = configuration;

        }
        [HttpPost]
        public IActionResult AddCategory(CategoryVm model)
        {
            var i = _categoryrepo.addcategory(model);
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
        public IActionResult AllCategoryInfo()
        {
            var users = _categoryrepo.GetCategoriesList();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var model = _categoryrepo.GetCategoryById(id);
                
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryEditVm model)
        {
            var i = _categoryrepo.EditCategory(model);
            if (i > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Not Updated");
            }
        }
        [HttpGet("{id}")]
        public IActionResult DeleteCategoryById(int id)
        {
            var i = _categoryrepo.deleteCategoryById(id);
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
