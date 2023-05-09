namespace ExpenseTracking.Core.Models.TransactionViewModels
{
    public class GetForEditTransactionViewModel : AddTransactionViewModel
    {
        public Guid Id { get; set; }
    }
}