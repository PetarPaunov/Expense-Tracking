namespace ExpenseTracking.Core.Contracts
{
    using ExpenseTracking.Core.Models.TransactionViewModels;

    public interface ITransactionService
    {
        public Task AddTransactionAsync(AddTransactionViewModel model, string userId);
        public Task<IEnumerable<GetUserTransactionsViewModel>> GetUserTransactionsAsync(string userId);
        public Task<GetUserTransactionsViewModel> GetTransactionInfoAsync(string transactionId, string userId);
        public Task<GetForEditTransactionViewModel> GetTransactionForEditAsync(string transactionId);
    }
}
