namespace ExpenseTracking.Core.Services
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Infrastructure.ExpenseTables.Wallet;
    using ExpenseTracking.Infrastructure.Models.ExpenseTables;
    using System.Text;

    public class CommonService : ICommonService
    {
        // Add comment

        public IEnumerable<ExpenseForDay> AddZeroExpenseForThePastDays(Wallet wallet)
        {
            var pastDays = GetPastDaysOfTheMonth();

            var expensesForDay = new List<ExpenseForDay>();

            for (int i = 0; i < pastDays; i++)
            {
                expensesForDay.Add(new ExpenseForDay()
                {
                    DayOfMonth = (i + 1).ToString(),
                    Expense = 0,
                    Wallet = wallet
                });
            }

            return expensesForDay;
        }

        // Add comment

        public IEnumerable<IncomeForDay> AddZeroIncomeForThePastDays(Wallet wallet)
        {
            var pastDays = GetPastDaysOfTheMonth();

            var incomesForDay = new List<IncomeForDay>();

            for (int i = 0; i < pastDays; i++)
            {
                incomesForDay.Add(new IncomeForDay()
                {
                    DayOfMonth = (i + 1).ToString(),
                    Income = 0,
                    Wallet = wallet
                });
            }

            return incomesForDay;
        }

        // Add comment
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

        // Add comment
        private static int GetPastDaysOfTheMonth()
        {
            var daysOfTheMonth = DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month);

            var difference = daysOfTheMonth - DateTime.UtcNow.Day;

            var pastDays = daysOfTheMonth - difference;

            return pastDays;
        }
    }
}