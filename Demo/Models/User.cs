using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [MaxLength(50)]
        [MinLength(5)]

        public string name { get; set; }  // Fixed casing (recommended)
        public string email { get; set; }
        public string password { get; set; }
        public int role_id { get; set; } 

        [ForeignKey("role_id")]


        public Role Role { get; set; } 

    }
}
