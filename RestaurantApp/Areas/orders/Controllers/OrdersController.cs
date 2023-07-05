using Microsoft.AspNetCore.Mvc;

namespace RestaurantApp.Areas.orders.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult OrdersDetails()
        {
            return View();
        }
    }
}
