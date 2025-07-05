using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Models
{
    public class Grade
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public float grade { get; set; }

        public int student_id { get; set; }
        public int assignment_id { get; set; }

        [ForeignKey(nameof(student_id))]
        [JsonIgnore]
        public User user { get; set; }

        [ForeignKey("assignment_id")]
        public Assignment assignment { get; set; }




    }





}
