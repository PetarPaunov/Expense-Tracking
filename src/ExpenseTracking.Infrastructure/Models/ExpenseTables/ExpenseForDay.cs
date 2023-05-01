namespace ExpenseTracking.Infrastructure.Models.ExpenseTables
{
    using ExpenseTracking.Infrastructure.ExpenseTables.Wallet;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ExpenseForDay
    {
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal Expense { get; set; }
        public int DayOfMonth { get; set; }

        public Guid WalletId { get; set; }

        [ForeignKey(nameof(WalletId))]
        public Wallet Wallet { get; set; } = null!;
    }
}