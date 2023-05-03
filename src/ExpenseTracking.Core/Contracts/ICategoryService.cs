namespace ExpenseTracking.Core.Contracts
{
    using ExpenseTracking.Core.Models.CategoryViewModels;

    public interface ICategoryService
    {
        public Task<IEnumerable<GetCategoriesViewModel>> GetCategoriesAsync();
    }
}
