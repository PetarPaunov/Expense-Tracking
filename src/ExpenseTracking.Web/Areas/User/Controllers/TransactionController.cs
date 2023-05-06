namespace ExpenseTracking.Web.Areas.User.Controllers
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Core.Models.TransactionViewModels;
    using ExpenseTracking.Web.Extensions;
    using Microsoft.AspNetCore.Mvc;

    public class TransactionController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly ITransactionService transactionService;

        public TransactionController(ICategoryService categoryService, 
                                     ITransactionService transactionService)
        {
            this.categoryService = categoryService;
            this.transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await this.categoryService.GetCategoriesAsync();

            this.ViewBag.Categories = categories;

            var model = new AddTransactionViewModel();

            return View(model);
        }

        // Add try catch and logger
        [HttpPost]
        public async Task<IActionResult> Index(AddTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = this.User.Id();

            await this.transactionService.AddTransactionAsync(model, userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
