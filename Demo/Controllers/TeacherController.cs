using Demo.DTOs;
using Demo.Models;
using Demo.Repository.AssignmentRepo;
using Demo.Repository.GradeRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        public readonly IAssignmentRepository assignmentRepository;
        public readonly IGradeRepository gradeRepository;

        public TeacherController( IAssignmentRepository assignmentRepository,IGradeRepository gradeRepository)
        {
            this.assignmentRepository = assignmentRepository;
            this.gradeRepository=gradeRepository;

        }


        [Authorize(Roles = "teacher")]
        [HttpPost("add_assignment")]
        public IActionResult AddAssignment([FromBody] AssignmentDTO assignmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdClaim.Value);

            var assignment = assignmentRepository.create(assignmentDTO, userId);

            return Ok(new { message = "Assignment added successfully", assignment });
        }

        [Authorize(Roles = "teacher")]
        [HttpPut("addGrade")]
        public IActionResult UpdateGrade([FromBody] SubmitAssignmentDTO grade)
        {
            if (grade == null)
            {
                return BadRequest(new { message = "Grade data is required." });
            }

            gradeRepository.update(grade);

            return Ok(new { message = "Grade updated successfully" });
        }
    }
}
