namespace ExpenseTracking.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserHomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
