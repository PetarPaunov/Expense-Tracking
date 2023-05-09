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

        // Add try catch and logger
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.Id();

            var categories = await this.categoryService.GetCategoriesAsync();
            var transactions = await this.transactionService.GetUserTransactionsAsync(userId);

            this.ViewBag.Categories = categories;
            this.ViewBag.Transactions = transactions;

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

        // Add try catch and logger
        [HttpGet]
        public async Task<IActionResult> InfoEdit(string id)
        {
            var userId = this.User.Id();
            
            var transaction = await this.transactionService.GetTransactionInfoAsync(id, userId);
            var categories = await this.categoryService.GetCategoriesAsync();

            this.ViewBag.Categories = categories;
            this.ViewBag.Transaction = transaction;

            var model = await this.transactionService.GetTransactionForEditAsync(id);

            return View(model);
        }
    }
}
