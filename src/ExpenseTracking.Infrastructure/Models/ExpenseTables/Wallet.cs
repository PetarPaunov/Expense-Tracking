namespace ExpenseTracking.Infrastructure.ExpenseTables.Wallet
{
    using ExpenseTracking.Infrastructure.Models.Account;
    using ExpenseTracking.Infrastructure.Models.Enums;
    using ExpenseTracking.Infrastructure.Models.ExpenseTables;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Wallet
    {
        public Wallet()
        {
            this.IncomeForDay = new HashSet<IncomeForDay>();
            this.ExpenseForDay = new HashSet<ExpenseForDay>();
        }

        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal Balance { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal Income { get; set; }
        public ICollection<IncomeForDay> IncomeForDay { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal Expence { get; set; }
        public ICollection<ExpenseForDay> ExpenseForDay { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal Savings { get; set; }

        public Currency Currency { get; set; } = Currency.EUR;

        public string ApplicationUserId { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}