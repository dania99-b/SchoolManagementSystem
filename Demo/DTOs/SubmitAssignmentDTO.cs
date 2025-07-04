using System.ComponentModel.DataAnnotations;

namespace Demo.DTOs
{
    public class SubmitAssignmentDTO
    {
        public int ?id { get; set; }
        [Required]
        public int AssignmentId { get; set; }
        public int?  grade { get; set; }

    }
}
