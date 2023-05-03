namespace ExpenseTracking.Core.Services
{
    using ExpenseTracking.Core.Contracts;
    using ExpenseTracking.Core.Models.CategoryViewModels;
    using ExpenseTracking.Infrastructure.GenericRepository;
    using ExpenseTracking.Infrastructure.Models.ExpenseTables;
    using Microsoft.EntityFrameworkCore;

    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository repository;

        public CategoryService(IGenericRepository repository)
        {
            this.repository = repository;
        }


        // Add comment
        public async Task<IEnumerable<GetCategoriesViewModel>> GetCategoriesAsync()
        {
            var categories = await this.repository
                .AllReadonly<Category>()
                .Select(x => new GetCategoriesViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Icon = x.Icon
                })
                .ToListAsync();

            return categories;
        }
    }
}