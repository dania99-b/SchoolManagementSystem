using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs
{
    public class AssignmentDTO
    {
        public int? Id { get; set; } // Optional: موجود في التحديث أو العرض فقط

        [Required]
        [MaxLength(200)]
        public string assignment_name { get; set; }

        public string? description { get; set; }

        [Required]
        public int course_id { get; set; }

        public DateTime? due_date { get; set; }

 
    }
}
