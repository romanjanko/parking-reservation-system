using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.WebUI.Models
{
    public class PasswordRecoveryViewModel
    {
        [Required(ErrorMessage = "Email address is a required field.")]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }
    }
}