using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs
{
    public class loginDTO
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
  
    }
}
