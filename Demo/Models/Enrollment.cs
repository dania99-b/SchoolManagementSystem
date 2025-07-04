using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public int user_id { get; set; }
        public int course_id { get; set; }
        [ForeignKey("user_id")]
        public User user { get; set; }
        [ForeignKey("course_id")]
        public Course course { get; set; }


    }
}
