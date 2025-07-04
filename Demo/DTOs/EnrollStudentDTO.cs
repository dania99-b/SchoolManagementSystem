using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs
{
    public class EnrollStudentDTO
    {
        [Required(ErrorMessage = "course_id is required")]
        public int ?course_id { get; set; }
        [Required(ErrorMessage = "student_id is required")]
        public int ?student_id { get; set; }
    }
}
