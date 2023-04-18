namespace ExpenseTracking.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
