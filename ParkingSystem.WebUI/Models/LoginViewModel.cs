using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email address is a required field.")]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is a required field.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}