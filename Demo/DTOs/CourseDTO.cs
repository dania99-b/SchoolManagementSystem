using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs
{
    public class CourseDTO
    {
        public int? id { get; set; }
        
        [StringLength(100, MinimumLength = 2)]
        public string ?course_name { get; set; }
        
        [Range(10, 20, ErrorMessage = "Course capacity must be between 10 and 20")]
        public int ?course_capacity { get; set; }
        [Required]
        public int user_id { get; set; }

    }
}
