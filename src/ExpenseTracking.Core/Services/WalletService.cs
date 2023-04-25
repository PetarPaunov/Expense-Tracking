namespace ExpenseTracking.Core.Services
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Core.Models.WalletViewModels;
    using ExpenseTracking.Infrastructure.ExpenseTables.Wallet;
    using ExpenseTracking.Infrastructure.GenericRepository;
    using ExpenseTracking.Infrastructure.Models.Account;
    using ExpenseTracking.Infrastructure.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using static ExpenseTracking.Core.Constants.CurrencySymbolConstants;
    using static ExpenseTracking.Core.Constants.ErrorConstants;

    public class WalletService : IWalletService
    {
        private readonly IGenericRepository repository;

        public WalletService(IGenericRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Obtaining user wallet information.
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        /// <returns>(Balance, Income, Expenses, Savings and Currency) from the user's wallet</returns>
        public async Task<WalletInformationViewModel> GetWalletInformationAsync(string userId)
        {
            var userWallet = await this.repository.AllReadonly<Wallet>()
                .FirstOrDefaultAsync(x => x.ApplicationUserId == userId);

            if (userWallet == null)
            {
                throw new ArgumentNullException(WalletNotFoundExeption);
            }

            var enumValue = Enum.GetName(typeof(Currency), userWallet.Currency);
            var currencySymbol = GetCurrencySymbol(enumValue);

            var walletInfo = new WalletInformationViewModel()
            {
                Balance = userWallet.Balance,
                Income = userWallet.Income,
                Expence = userWallet.Expence,
                Savings = userWallet.Savings,
                Currency = enumValue != null ? enumValue : "COM",
                CurrencySymbol = currencySymbol,
            };

            return walletInfo;
        }

        private string GetCurrencySymbol(string currentCurrency)
        {
            var currencySymbol = "";

            foreach (var currency in currencyArray)
            {
                if (currency.Key == currentCurrency)
                {
                    currencySymbol = currency.Value;
                }
            }

            return currencySymbol;
        }
    }
}