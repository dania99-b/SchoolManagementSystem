using Demo.DTOs;
using Demo.Models;
using Demo.Repository.AssignmentRepo;
using Demo.Repository.EnrollmentRepository;
using Demo.Repository.GradeRepo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IGradeRepository gradeRepository;
        private readonly IAssignmentRepository assignmentRepository;
        private readonly IEnrollmentRepository enrollmentRepository;
        public StudentController(IGradeRepository gradeRepository,IAssignmentRepository assignmentRepositoryc,IEnrollmentRepository enrollmentRepository)
        {
            this.gradeRepository = gradeRepository;
            this.assignmentRepository=assignmentRepositoryc;
            this.enrollmentRepository = enrollmentRepository;
        }

        [HttpPost("assignments/submit")]
        [Authorize(Roles = "student")]
        public IActionResult SubmitAssignment([FromBody] SubmitAssignmentDTO submission)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);

           
            var assignment = assignmentRepository.Get(submission.AssignmentId);
            if (assignment == null)
            {
                return NotFound(new { message = "Assignment not found." });
            }

          
            var enrolled = enrollmentRepository.GetAllEnrollments()
                             .Any(e => e.user_id == userId && e.course_id == assignment.course_id);
            if (!enrolled)
            {
                return BadRequest(new { message = "You are not enrolled in the course for this assignment." });
            }


            var exists = gradeRepository.GetAllGrade()
                          .FirstOrDefault(g => g.assignment_id == submission.AssignmentId && g.student_id == userId);
            if (exists != null)
                return BadRequest(new { message = "You have already submitted this assignment." });

            // إنشاء التقديم
            var newSub = gradeRepository.create(submission, userId);

            return Ok(newSub);
        }


    }
}


    

