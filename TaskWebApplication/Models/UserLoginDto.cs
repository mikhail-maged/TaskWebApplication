using System.ComponentModel.DataAnnotations;

namespace JWTAuthorization.Models
{
    public class UserLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }    
    }
}
