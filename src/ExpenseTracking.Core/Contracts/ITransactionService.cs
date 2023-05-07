namespace ExpenseTracking.Core.Contracts
{
    using ExpenseTracking.Core.Models.TransactionViewModels;

    public interface ITransactionService
    {
        public Task AddTransactionAsync(AddTransactionViewModel model, string userId);
        public Task<IEnumerable<GetUserTransactionsViewModel>> GetUserTransactionsAsync(string userId);
    }
}
