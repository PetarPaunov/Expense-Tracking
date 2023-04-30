namespace ExpenseTracking.Core.Contracts
{
    using ExpenseTracking.Core.Models.WalletViewModels;

    public interface IWalletService
    {
        public Task<WalletInformationViewModel> GetWalletInformationAsync(string userId);
        public Task<string[]> GetExpensesAndIncomesForDays(string userId);
    }
}