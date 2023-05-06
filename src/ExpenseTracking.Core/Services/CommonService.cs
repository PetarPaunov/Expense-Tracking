namespace ExpenseTracking.Core.Services
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Infrastructure.ExpenseTables.Wallet;
    using ExpenseTracking.Infrastructure.Models.ExpenseTables;
    using System.Text;

    public class CommonService : ICommonService
    {
        /// <summary>
        /// Adds zero(0) expenses for the past days of the month
        /// </summary>
        /// <param name="wallet">Logged-in user's wallet</param>
        /// <returns>All initial expenses</returns>
        public IEnumerable<ExpenseForDay> AddZeroExpenseForThePastDays(Wallet wallet)
        {
            var pastDays = GetPastDaysOfTheMonth();

            var expensesForDay = new List<ExpenseForDay>();

            for (int i = 0; i < pastDays; i++)
            {
                expensesForDay.Add(new ExpenseForDay()
                {
                    DayOfMonth = i + 1,
                    Expense = 0,
                    Wallet = wallet
                });
            }

            return expensesForDay;
        }

        /// <summary>
        /// Adds zero(0) incomes for the past days of the month
        /// </summary>
        /// <param name="wallet">Logged-in user's wallet</param>
        /// <returns>All initial incomes</returns>
        public IEnumerable<IncomeForDay> AddZeroIncomeForThePastDays(Wallet wallet)
        {
            var pastDays = GetPastDaysOfTheMonth();

            var incomesForDay = new List<IncomeForDay>();

            for (int i = 0; i < pastDays; i++)
            {
                incomesForDay.Add(new IncomeForDay()
                {
                    DayOfMonth = i + 1,
                    Income = 0,
                    Wallet = wallet
                });
            }

            return incomesForDay;
        }

        /// <summary>
        /// Gets all days of the month
        /// </summary>
        /// <returns>All days of the month as a string to be displayed in the chart</returns>
        public string GetDaysOfTheMonth()
        {
            var daysOfTheMonth = DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month);

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < daysOfTheMonth; i++)
            {
                stringBuilder.Append((i + 1) + ", ");
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets all past days of the month
        /// </summary>
        /// <returns>All past days of the month</returns>
        public int GetPastDaysOfTheMonth()
        {
            var daysOfTheMonth = DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month);

            var difference = daysOfTheMonth - DateTime.UtcNow.Day;

            var pastDays = daysOfTheMonth - difference;

            return pastDays;
        }
    }
}