using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.WebUI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email address is a required field.")]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "New password is a required field.")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is a required field.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirm new password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}