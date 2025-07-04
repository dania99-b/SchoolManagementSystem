using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs
{
    public class RegisterDTO
    {

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string name { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com)$", ErrorMessage = "Email must end with '.com'")]
        public string email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string password { get; set; }
    }
}
