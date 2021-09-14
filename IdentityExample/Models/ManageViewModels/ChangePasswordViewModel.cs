using System.ComponentModel.DataAnnotations;

namespace IdentityExample.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [StringLength(100, ErrorMessage = "The {0} field must have a minimum of {2} and a maximum of {1} characters.", MinimumLength = 8)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords must match.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
