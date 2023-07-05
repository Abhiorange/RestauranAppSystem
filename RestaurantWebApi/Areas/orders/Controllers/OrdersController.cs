using Microsoft.AspNetCore.Mvc;

namespace RestaurantWebApi.Areas.orders.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
