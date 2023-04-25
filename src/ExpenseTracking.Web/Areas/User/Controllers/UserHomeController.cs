namespace ExpenseTracking.Web.Areas.User.Controllers
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Web.Extensions;
    using Microsoft.AspNetCore.Mvc;

    public class UserHomeController : BaseController
    {
        private readonly IWalletService walletService;

        public UserHomeController(IWalletService walletService)
        {
            this.walletService = walletService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.Id();

            // Add logger and try catch blocks

            var model = await this.walletService.GetWalletInformationAsync(userId);

            return View(model);
        }
    }
}
