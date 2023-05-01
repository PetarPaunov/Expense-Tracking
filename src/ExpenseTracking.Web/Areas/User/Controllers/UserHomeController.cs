namespace ExpenseTracking.Web.Areas.User.Controllers
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Web.Extensions;
    using Microsoft.AspNetCore.Mvc;

    public class UserHomeController : BaseController
    {
        private readonly IWalletService walletService;
        private readonly ICommonService commonService;

        public UserHomeController(IWalletService walletService,
                                  ICommonService commonService)
        {
            this.walletService = walletService;
            this.commonService = commonService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.Id();

            // Add logger and try catch blocks

            await this.walletService.AddNewDailyExpenseAndIncome(userId);

            var model = await this.walletService.GetWalletInformationAsync(userId);
            var dayOfTheMonth = this.commonService.GetDaysOfTheMonth();

            var incomesExpensesForDay = await this.walletService.GetExpensesAndIncomesForDays(userId);

            this.ViewBag.DaysOfTheMonth = dayOfTheMonth;
            this.ViewBag.ExpnsesForDay = incomesExpensesForDay[0];
            this.ViewBag.IncomesForDay = incomesExpensesForDay[1];

            return View(model);
        }
    }
}
