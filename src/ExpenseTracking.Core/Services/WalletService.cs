namespace ExpenseTracking.Core.Services
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Core.Models.WalletViewModels;
    using ExpenseTracking.Infrastructure.ExpenseTables.Wallet;
    using ExpenseTracking.Infrastructure.GenericRepository;
    using ExpenseTracking.Infrastructure.Models.Account;
    using ExpenseTracking.Infrastructure.Models.Enums;
    using ExpenseTracking.Infrastructure.Models.ExpenseTables;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using static ExpenseTracking.Core.Constants.CurrencySymbolConstants;
    using static ExpenseTracking.Core.Constants.ErrorConstants;

    public class WalletService : IWalletService
    {
        private readonly IGenericRepository repository;
        private readonly ICommonService commonService;

        public WalletService(IGenericRepository repository, 
                             ICommonService commonService)
        {
            this.repository = repository;
            this.commonService = commonService;
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
            var currencySymbol = this.commonService.GetCurrencySymbol(enumValue);

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

        /// <summary>
        /// Gets all expenses and incomes of the user
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        /// <returns>All expenses and incomes of the user as a string to be able to be set in the chart</returns>
        /// <exception cref="ArgumentNullException">If the user wallet is not found</exception>
        public async Task<string[]> GetExpensesAndIncomesForDays(string userId)
        {
            var userWallet = await this.repository.AllReadonly<Wallet>()
               .Include(x => x.IncomeForDay)
               .Include(x => x.ExpenseForDay)
               .FirstOrDefaultAsync(x => x.ApplicationUserId == userId);

            if (userWallet == null)
            {
                throw new ArgumentNullException(WalletNotFoundExeption);
            }

            var expenseAndIncomeForDay = new string[2];

            var expensesForDay = new StringBuilder();
            var incomesForDay = new StringBuilder();

            foreach (var expense in userWallet.ExpenseForDay)
            {
                expensesForDay.Append(expense.Expense + ", ");
            }

            expenseAndIncomeForDay[0] = expensesForDay.ToString();

            foreach (var income in userWallet.IncomeForDay)
            {
                incomesForDay.Append(income.Income + ", ");
            }

            expenseAndIncomeForDay[1] = incomesForDay.ToString();

            return expenseAndIncomeForDay;
        }

        /// <summary>
        /// Adds new initial income and expenses for the day. If it is the same day, the method does nothing.
        /// If more days are past adds the difference between last income or expense and current day of the month.
        /// If it is the next day of the month, adds new initial revenue and expenses for the day.
        /// If it is the next month, removes all income and expenses and adds new initial
        /// </summary>
        /// <param name="userId">Unique user identifier</param>
        /// <returns>void</returns>
        /// <exception cref="ArgumentNullException">If the user wallet is not found</exception>
        public async Task AddNewDailyExpenseAndIncome(string userId)
        {
            var userWallet = await this.repository.All<Wallet>()
               .Include(x => x.IncomeForDay)
               .Include(x => x.ExpenseForDay)
               .FirstOrDefaultAsync(x => x.ApplicationUserId == userId);

            if (userWallet == null)
            {
                throw new ArgumentNullException(WalletNotFoundExeption);
            }

            var incomeDayOfMonth = userWallet.IncomeForDay
                .OrderByDescending(x => x.DayOfMonth)
                .First();

            var expenseDayOfMonth = userWallet.ExpenseForDay
                .OrderByDescending(x => x.DayOfMonth)
                .First();

            var thoday = DateTime.UtcNow.Day;

            if (thoday == incomeDayOfMonth.DayOfMonth
                && thoday == expenseDayOfMonth.DayOfMonth)
            {
                return;
            }

            if (thoday < incomeDayOfMonth.DayOfMonth
                && thoday < expenseDayOfMonth.DayOfMonth)
            {
                userWallet.IncomeForDay.Clear();
                userWallet.ExpenseForDay.Clear();
            }

            var userHasNotLoggedInDays = GetPastDaysTheUserHasNotLoggedIn(incomeDayOfMonth.DayOfMonth);

            for (int i = 0; i < userHasNotLoggedInDays; i++)
            {
                userWallet.IncomeForDay.Add(new IncomeForDay()
                {
                    DayOfMonth = incomeDayOfMonth.DayOfMonth + i + 1,
                    Income = 0,
                    Wallet = userWallet,
                    WalletId = userWallet.Id,
                });

                userWallet.ExpenseForDay.Add(new ExpenseForDay()
                {
                    DayOfMonth = expenseDayOfMonth.DayOfMonth + i + 1,
                    Expense = 0,
                    Wallet = userWallet,
                    WalletId = userWallet.Id,
                });
            }

            await this.repository.SaveChangesAsync();
        }

        // Add comment
        private int GetPastDaysTheUserHasNotLoggedIn(int lastIncmeDayOfMonth)
        {
            var pastDays = this.commonService.GetPastDaysOfTheMonth();

            var difference = pastDays - lastIncmeDayOfMonth;

            return difference;
        }
    }
}