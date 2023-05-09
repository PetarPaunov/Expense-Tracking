namespace ExpenseTracking.Core.Constants
{
    public static class ErrorConstants
    {
        public const string SomethingWentWrong = "Something went wrong!";
        public const string UserNameErrorMessage = "Field must be between 4 and 20 symbols.";
        public const string PasswordErrorMessage = "Field must be between 6 and 20 symbols.";

        public const string UserNotFoundExeption = "This user does not exist!";
        public const string WalletNotFoundExeption = "Wallet does not exist!";
        public const string CategoryNotFoundExeption = "This category does not exist!";
        public const string TransactionNotFoundExeption = "This transaction does not exist!";
    }
}