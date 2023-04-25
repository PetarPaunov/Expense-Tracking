namespace ExpenseTracking.Core.Models.WalletViewModels
{
    using ExpenseTracking.Infrastructure.Models.Enums;

    public class WalletInformationViewModel
    {
        public decimal Balance { get; set; }
        public decimal Income { get; set; }
        public decimal Expence { get; set; }
        public decimal Savings { get; set; }
        public string Currency { get; set; } = null!;
        public string CurrencySymbol { get; set; } = null!;
    }
}