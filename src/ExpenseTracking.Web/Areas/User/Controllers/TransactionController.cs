namespace ExpenseTracking.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class TransactionController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
