namespace ExpenseTracking.Core.Models.TransactionViewModels
{
    public class GetUserTransactionsViewModel
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public string? Note { get; set; }
        public string CategoryTitleAndIcon { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string CurrencySymbol { get; set; } = null!;
    }
}