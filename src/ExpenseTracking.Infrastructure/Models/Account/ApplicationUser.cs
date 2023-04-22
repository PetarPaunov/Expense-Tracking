namespace ExpenseTracking.Infrastructure.Models.Account
{
    using ExpenseTracking.Infrastructure.ExpenseTables.Wallet;
    using ExpenseTracking.Infrastructure.Models.ExpenseTables;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Transactions = new HashSet<Transaction>();
        }

        public Wallet Wallet { get; set; } = null!;
        public ICollection<Transaction> Transactions { get; set; }
    }
}