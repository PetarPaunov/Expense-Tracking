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

    public class TransactionService : ITransactionService
    {
        private readonly IGenericRepository repository;

        public TransactionService(IGenericRepository repository)
        {
            this.repository = repository;
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

            user.Wallet.Balance += (decimal)model.Amount;

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
            }
            else
            {
                user.Wallet.Income += (decimal)model.Amount;
                incomeDayOfMonth.Income += (decimal)model.Amount;
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
    }
}