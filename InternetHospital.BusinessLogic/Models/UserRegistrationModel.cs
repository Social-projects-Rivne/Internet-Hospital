using System.ComponentModel.DataAnnotations;

namespace InternetHospital.BusinessLogic.Models
{
    public class UserRegistrationModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email!")]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{8,24}$", ErrorMessage = "Password must be at least 8 characters and contain digits, upper and lower case")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are different")]
        public string ConfirmPassword { get; set; }
    }
}
