namespace ExpenseTracking.Infrastructure.Models.ExpenseTables
{
    using ExpenseTracking.Infrastructure.Models.Account;
    using ExpenseTracking.Infrastructure.Models.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static ExpenseTracking.Infrastructure.Constants.DataConstants.TransactionConstants;

    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = AmountErrorMessage)]
        public double Amount { get; set; }

        public string? Note { get; set; }

        public Guid CategotyId { get; set; }

        [ForeignKey(nameof(CategotyId))]
        public Category Category { get; set; } = null!;

        public Type Type { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public string ApplicationUserId { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}