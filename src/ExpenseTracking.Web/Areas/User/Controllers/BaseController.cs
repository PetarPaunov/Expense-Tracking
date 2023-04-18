namespace ExpenseTracking.Web.Areas.User.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static ExpenseTracking.Core.Contracts.RoleConstants;

    [Authorize]
    [Area("User")]
    [Authorize(Roles = UserRole)]
    public class BaseController : Controller
    {
    }
}