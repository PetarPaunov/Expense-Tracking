namespace ExpenseTracking.Core.Models.CategoryViewModels
{
    public class GetCategoriesViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Icon { get; set; }
    }
}