using System.ComponentModel.DataAnnotations;

namespace JWTAuthorization.Models
{
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password Not matched")]
        public string ComfirmPassword { get; set; }
    }
}
