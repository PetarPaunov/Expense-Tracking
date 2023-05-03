namespace ExpenseTracking.Web.Areas.User.Controllers
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Core.Models.TransactionViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class TransactionController : BaseController
    {
        private readonly ICategoryService categoryService;

        public TransactionController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await this.categoryService.GetCategoriesAsync();

            this.ViewBag.Categories = categories;

            var model = new AddTransactionViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AddTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }



            return RedirectToAction(nameof(Index));
        }
    }
}
