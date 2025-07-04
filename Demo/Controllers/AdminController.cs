using Demo.DTOs;
using Demo.Models;
using Demo.Repository.AssignmentRepo;
using Demo.Repository.CourseRepo;
using Demo.Repository.GradeRepo;
using Demo.Repository.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly ICourseRepository courseRepository;
        public readonly IUserRepository userRepository;
        public readonly IAssignmentRepository assignmentRepository;
        public readonly IGradeRepository gradeRepository;


        public AdminController(ICourseRepository courseRepository, IUserRepository userRepository, IAssignmentRepository assignmentRepository, IGradeRepository gradeRepository)
        {
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
            this.assignmentRepository = assignmentRepository;
            this.gradeRepository = gradeRepository;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("enroll_student")]
        public IActionResult enroll_student([FromBody] EnrollStudentDTO data)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(400, new
                {
                    message = "Validation failed",
                    errors = ModelState
                });
            }

            var existing_user = userRepository
                .GetAllUser()
                .FirstOrDefault(u => u.id == data.student_id && u.Role.name == "student");

            var existing_course = courseRepository
               .GetAllCourse()
               .FirstOrDefault(c => c.id == data.course_id);

            var existing_enrollment = courseRepository
              .getEnrollStudent()
              .FirstOrDefault(es => es.user_id == data.student_id && es.course_id==data.course_id);
            if (existing_user == null)
            {
                return BadRequest(new
                {
                    message = "Validation failed",
                    errors = ModelState
                });
            }
            if (existing_course == null)
            {
                return NotFound(new
                {
                    message = $"Course with ID {data.course_id} does not exist."
                });
            }

            if (existing_enrollment!=null)
            {
                return Conflict(new
                {
                    message = "The enrollment already exists."
                });
            }


            Enrollment enrollment = courseRepository.EnrollStudent((int)data.course_id, (int)data.student_id);

            return Ok(new
            {
                message = "Student enrolled successfully",
                data = enrollment
            });



        }

    }
    
}
