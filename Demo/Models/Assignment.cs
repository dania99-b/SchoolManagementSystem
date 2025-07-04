using Demo.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Demo.Models
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string assignment_name { get; set; }
        public int course_id { get; set; }
        public string description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int teacher_id { get; set; } // teacher


        [ForeignKey("course_id")]
        [JsonIgnore]
        public Course course { get; set; }
        [ForeignKey(nameof(teacher_id))]
        [JsonIgnore]
        public User Teacher { get; set; }
    }
}
