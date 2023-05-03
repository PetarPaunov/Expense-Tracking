namespace ExpenseTracking.Core.Models.TransactionViewModels
{
    public class AddTransactionViewModel
    {
        public double Amount { get; set; }
        public string? Note { get; set; }
        public Guid CategoryId { get; set; }
        public string Type { get; set; } = null!;
    }
}