namespace ExpenseTracking.Infrastructure.Models.Account
{
    using Microsoft.AspNetCore.Identity;
    using ExpenseTracking.Infrastructure.Models.Wallet;

    public class ApplicationUser : IdentityUser
    {
        public Wallet Wallet { get; set; } = null!;
    }
}