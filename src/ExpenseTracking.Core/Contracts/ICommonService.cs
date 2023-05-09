namespace ExpenseTracking.Core.Contracts
{
    using ExpenseTracking.Infrastructure.ExpenseTables.Wallet;
    using ExpenseTracking.Infrastructure.Models.ExpenseTables;

    public interface ICommonService
    {
        public string GetDaysOfTheMonth();
        public IEnumerable<IncomeForDay> AddZeroIncomeForThePastDays(Wallet wallet);
        public IEnumerable<ExpenseForDay> AddZeroExpenseForThePastDays(Wallet wallet);
        public int GetPastDaysOfTheMonth();
        public string GetCurrencySymbol(string currentCurrency);
    }
}