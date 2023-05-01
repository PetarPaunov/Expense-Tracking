namespace ExpenseTracking.Web.Areas.User.Controllers
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Web.Extensions;
    using Microsoft.AspNetCore.Mvc;

    public class UserHomeController : BaseController
    {
        private readonly IWalletService walletService;
        private readonly ICommonService commonService;
        private readonly ILogger<UserHomeController> logger;

        public UserHomeController(IWalletService walletService,
                                  ICommonService commonService,
                                  ILogger<UserHomeController> logger)
        {
            this.walletService = walletService;
            this.commonService = commonService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = this.User.Id();

                await this.walletService.AddNewDailyExpenseAndIncome(userId);

                var model = await this.walletService.GetWalletInformationAsync(userId);
                var dayOfTheMonth = this.commonService.GetDaysOfTheMonth();

                var incomesExpensesForDay = await this.walletService.GetExpensesAndIncomesForDays(userId);

                this.ViewBag.DaysOfTheMonth = dayOfTheMonth;
                this.ViewBag.ExpnsesForDay = incomesExpensesForDay[0];
                this.ViewBag.IncomesForDay = incomesExpensesForDay[1];

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);

                // Add correnct Error page!!!
                return RedirectToAction("ErrorSomething", "Home");
            }

        }
    }
}
