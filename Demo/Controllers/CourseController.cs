using Demo.DTOs;
using Demo.Models;
using Demo.Repository.AssignmentRepo;
using Demo.Repository.CourseRepo;
using Demo.Repository.GradeRepo;
using Demo.Repository.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {

      
        public readonly ICourseRepository courseRepository;
        public readonly IUserRepository userRepository;
        public readonly IAssignmentRepository assignmentRepository;
        public readonly IGradeRepository gradeRepository;

        public CourseController(ICourseRepository courseRepository,IUserRepository userRepository,IAssignmentRepository assignmentRepository,IGradeRepository gradeRepository)
        {
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
            this.assignmentRepository = assignmentRepository;
            this.gradeRepository = gradeRepository;
        }
        [Authorize(Roles = "teacher,admin")]
        [HttpPost("create")]
        // GET: RoomController/Create
        public ActionResult Create([FromBody] CourseDTO course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            System.Diagnostics.Debug.WriteLine($"UserId from token: {userId}");
            var createdCourse = courseRepository.create(course, userId);
            return CreatedAtAction(nameof(Get), new { id = createdCourse.id }, createdCourse);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var course = courseRepository.Get(id);
            return Ok(course);
        }

        [Authorize(Roles = "teacher,admin")]
        [HttpPut("update/{id:int}")]
        public IActionResult Edit(int id, [FromBody] CourseDTO course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;


            var existingCourse = courseRepository.Get(id);
            if (existingCourse == null)
            {
                return NotFound(new { message = "Course not found." });
            }

           //هنا نتأكد بأن الاستاذ المسؤول عن هذ الكورس هو من يقوم بالتعديل او الادمن
            if (roleClaim != "admin"&&existingCourse.user_id != userId)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "You are not authorized to update this course.");
            }


            courseRepository.update(id, course);
                return Ok(new { message = "Course updated successfully" });
            
          
        }

        [Authorize(Roles = "teacher,admin")]
        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;


            var existingCourse = courseRepository.Get(id);
            if (existingCourse == null)
            {
                return NotFound("Course not found.");
            }

            if (roleClaim != "admin" && existingCourse.user_id != userId)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "You are not authorized to delete this course.");
            }
            courseRepository.delete(id);
            return Ok(new { message = "Course deleted successfully" });
        }
        [Authorize(Roles = "teacher,admin,student")]
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            List<Course> courses;
            if (role == "student")
            {
                courses = courseRepository.GetCoursesByStudentId(userId);
            }
            else
            {
                
                courses = courseRepository.GetAllCourse();
            }

            return Ok(courses);
        }






    }
}
