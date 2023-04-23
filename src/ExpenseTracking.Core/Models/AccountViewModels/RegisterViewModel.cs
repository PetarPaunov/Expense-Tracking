namespace ExpenseTracking.Core.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static ExpenseTracking.Core.Constants.AccountConstants;
    using static ExpenseTracking.Core.Constants.ErrorConstants;

    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserNameMaxLength, MinimumLength = UserNameMinLength, ErrorMessage = UserNameErrorMessage)]
        public string UserName { get; set; } = null!;

        [Required]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = PasswordErrorMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}