namespace ExpenseTracking.Infrastructure.Models.ExpenseTables
{
    using System.ComponentModel.DataAnnotations;

    using static ExpenseTracking.Infrastructure.Constants.DataConstants.CategoryConstants;
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = CategoryTitleErrorMessage)]
        [MinLength(CategoryTitleMinLenght)]
        public string Title { get; set; } = null!;

        public string? Icon { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;
    }
}