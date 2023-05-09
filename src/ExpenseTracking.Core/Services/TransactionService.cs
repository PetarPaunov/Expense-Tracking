namespace ExpenseTracking.Core.Services
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Core.Models.TransactionViewModels;
    using ExpenseTracking.Infrastructure.GenericRepository;
    using ExpenseTracking.Infrastructure.Models.Account;
    using ExpenseTracking.Infrastructure.Models.ExpenseTables;
    using ExpenseTracking.Infrastructure.Models.Enums;

    using static ExpenseTracking.Core.Constants.ErrorConstants;
    using Microsoft.EntityFrameworkCore;
    using ExpenseTracking.Infrastructure.ExpenseTables.Wallet;
    using System.Collections.Generic;

    public class TransactionService : ITransactionService
    {
        private readonly IGenericRepository repository;
        private readonly ICommonService commonService;

        public TransactionService(IGenericRepository repository, 
                                  ICommonService commonService)
        {
            this.repository = repository;
            this.commonService = commonService;
        }

        // Add comment
        public async Task AddTransactionAsync(AddTransactionViewModel model, string userId)
        {
            var user = await this.repository.All<ApplicationUser>()
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            var userWallet = await this.repository.All<Wallet>()
                .Include(x => x.IncomeForDay)
                .Include(x => x.ExpenseForDay)
                .FirstOrDefaultAsync(x => x.ApplicationUserId == userId);

            if (userWallet == null)
            {
                throw new ArgumentNullException(WalletNotFoundExeption);
            }

            if (user == null)
            {
                throw new ArgumentNullException(UserNotFoundExeption);
            }

            var category = await this.repository.GetByIdAsync<Category>(model.CategoryId);

            if (category == null)
            {
                throw new ArgumentNullException(CategoryNotFoundExeption);
            }

            var enumValue = Enum.Parse<Type>(model.Type);

            var incomeDayOfMonth = userWallet.IncomeForDay
                .OrderByDescending(x => x.DayOfMonth)
                .First();

            var expenseDayOfMonth = userWallet.ExpenseForDay
                .OrderByDescending(x => x.DayOfMonth)
                .First();

            if (enumValue == Type.Expense)
            {
                user.Wallet.Expence += (decimal)model.Amount;
                expenseDayOfMonth.Expense += (decimal)model.Amount;
                user.Wallet.Balance -= (decimal)model.Amount;
            }
            else
            {
                user.Wallet.Income += (decimal)model.Amount;
                incomeDayOfMonth.Income += (decimal)model.Amount;
                user.Wallet.Balance += (decimal)model.Amount;
            }

            var newTransaction = new Transaction()
            {
                Amount = model.Amount,
                ApplicationUser = user,
                ApplicationUserId = userId,
                CategotyId = model.CategoryId,
                Category = category,
                Note = model.Note,
                Type = enumValue,
            };

            await this.repository.AddAsync<Transaction>(newTransaction);
            await this.repository.SaveChangesAsync();
        }

        // Add comment
        public async Task<GetForEditTransactionViewModel> GetTransactionForEditAsync(string transactionId)
        {
            var guidTransactionId = new Guid(transactionId);

            var transaction = await this.repository
                .AllReadonly<Transaction>()
                .Where(x => x.Id == guidTransactionId)
                .Select(x => new GetForEditTransactionViewModel()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Note = x.Note,
                    Type = x.Type.ToString(),
                    CategoryId = x.CategotyId,
                })
                .FirstOrDefaultAsync();

            if (transaction == null)
            {
                throw new ArgumentNullException(TransactionNotFoundExeption);
            }

            return transaction;
        }

        // Add comment
        public async Task<GetUserTransactionsViewModel> GetTransactionInfoAsync(string transactionId, string userId)
        {
            var userWallet = await this.repository.AllReadonly<Wallet>()
                .FirstOrDefaultAsync(x => x.ApplicationUserId == userId);

            if (userWallet == null)
            {
                throw new ArgumentNullException(WalletNotFoundExeption);
            }

            var enumValue = Enum.GetName(typeof(Currency), userWallet.Currency);
            var currencySymbol = this.commonService.GetCurrencySymbol(enumValue);

            var guidTransactionId = new Guid(transactionId);

            var transaction = await this.repository
                .AllReadonly<Transaction>()
                .Where(x => x.Id == guidTransactionId)
                .Select(x => new GetUserTransactionsViewModel()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Note = x.Note,
                    Type = x.Type.ToString(),
                    CurrencySymbol = currencySymbol,
                    CategoryTitleAndIcon = x.Category.Icon + " " + x.Category.Title
                })
                .FirstOrDefaultAsync();

            if (transaction == null)
            {
                throw new ArgumentNullException(TransactionNotFoundExeption);
            }

            return transaction;
        }

        // Add comment
        public async Task<IEnumerable<GetUserTransactionsViewModel>> GetUserTransactionsAsync(string userId)
        {
            var userWallet = await this.repository.AllReadonly<Wallet>()
                .FirstOrDefaultAsync(x => x.ApplicationUserId == userId);

            if (userWallet == null)
            {
                throw new ArgumentNullException(WalletNotFoundExeption);
            }

            var enumValue = Enum.GetName(typeof(Currency), userWallet.Currency);
            var currencySymbol = this.commonService.GetCurrencySymbol(enumValue);

            var userTransactions = await this.repository
                .AllReadonly<Transaction>()
                .Where(x => x.ApplicationUserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new GetUserTransactionsViewModel()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    Note = x.Note,
                    Type = x.Type.ToString(),
                    CurrencySymbol = currencySymbol,
                    CategoryTitleAndIcon = x.Category.Icon + " " + x.Category.Title
                })
                .ToListAsync();

            return userTransactions;
        }
    }
}