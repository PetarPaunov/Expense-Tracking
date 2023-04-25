namespace ExpenseTracking.Core.Constants
{
    public static class CurrencySymbolConstants
    {
        private const string BGN = "лв";
        private const string TRY = "₺";
        private const string USD = "$";
        private const string EUR = "€";

        public static Dictionary<string, string> currencyArray = new Dictionary<string, string>()
        {
            {nameof(BGN), BGN },
            {nameof(TRY), TRY },
            {nameof(USD), USD },
            {nameof(EUR), EUR },
        };
    }
}