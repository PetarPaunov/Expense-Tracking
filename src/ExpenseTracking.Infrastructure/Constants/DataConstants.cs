namespace ExpenseTracking.Infrastructure.Constants
{
    /// <summary>
    /// Constants for database models
    /// </summary>
    public static class DataConstants
    {
        public static class TransactionConstants
        {
            public const string AmountErrorMessage = "Amount should be greater than 0.";
        }

        public static class CategoryConstants
        {
            public const int CategoryTitleMinLenght = 15;
            public const string CategoryTitleErrorMessage = "Title is required.";
        }
        // Add static classes to hold the constants
    }
}
