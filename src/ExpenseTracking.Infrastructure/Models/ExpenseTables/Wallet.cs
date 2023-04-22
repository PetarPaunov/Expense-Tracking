namespace ExpenseTracking.Infrastructure.ExpenseTables.Wallet
{
    using ExpenseTracking.Infrastructure.Models.Account;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Wallet
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal Balance { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal Income { get; set; }

        [Column(TypeName = "decimal(14, 2)")]
        public decimal Expence { get; set; }

        public string ApplicationUserId { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}