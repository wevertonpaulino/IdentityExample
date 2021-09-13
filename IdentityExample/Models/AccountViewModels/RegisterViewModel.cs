using System.ComponentModel.DataAnnotations;

namespace IdentityExample.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} field must have a minimum of {2} and a maximum of {1} characters.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }
    }
}
