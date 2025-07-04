using Demo.DTOs;
using Demo.Models;
using Demo.Repository.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Demo.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthenticationController(IUserRepository userRepository , IConfiguration config) { 
            _config = config;
            _userRepository = userRepository; }

        [HttpPost("register/student")]
        public IActionResult RegisterStudent([FromBody] RegisterDTO student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Validation failed", errors = ModelState });
            }
            var existing_user = _userRepository
                .GetAllUser()
                .FirstOrDefault(u => u.email == student.email);

            if (existing_user != null)
            {
                return Conflict(new { message = "Email already registered." });

            }

            // Hash the password
            string hashedPassword = SessionHelper.HashPassword(student.password);


            // Create user
            User newStudent = new User
            {
                name = student.name,
                email = student.email,
                password = hashedPassword,
                role_id = 1
            };

            _userRepository.create(newStudent);

          

            return Ok(new
            {
                Message = "Registration successful",
                UserId = newStudent.id
            });
        }



        [HttpPost("register/teacher")]
        public IActionResult RegisterTeacher([FromBody] RegisterDTO teacher)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Validation failed", errors = ModelState });
            }
            var existing_teacher = _userRepository
                .GetAllUser()
                .FirstOrDefault(u => u.email == teacher.email);

            if (existing_teacher != null)
            {
                return Conflict(new { message = "Email already registered." });
            }

            // Hash the password
            string hashedPassword = SessionHelper.HashPassword(teacher.password);


            // Create user
            User newTeacher = new User
            {
                name = teacher.name,
                email = teacher.email,
                password = hashedPassword,
                role_id = 2
            };

            _userRepository.create(newTeacher);



            return Ok(new
            {
                message = "Registration successful",
                userId = newTeacher.id
            });
        }


        [HttpPost("register/admin")]
        public IActionResult RegisterAdmin([FromBody] RegisterDTO admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Validation failed", errors = ModelState });
            }

            var existing_admin = _userRepository
                .GetAllUser()
                .FirstOrDefault(u => u.email == admin.email);

            if (existing_admin != null)
            {
                return Conflict(new { message = "Email already registered." });
            }

            // Hash the password
            string hashedPassword = SessionHelper.HashPassword(admin.password);


            // Create user
            User newAdmin = new User
            {
                name = admin.name,
                email = admin.email,
                password = hashedPassword,
                role_id = 3
            };

            _userRepository.create(newAdmin);



            return Ok(new
            {
                message = "Registration successful",
                userId = newAdmin.id
            });
        }










        [HttpPost("login")]
        public IActionResult Login([FromBody] loginDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Validation failed", errors = ModelState });
            }

            var user_exist = _userRepository.GetAllUser()
                .FirstOrDefault(u => u.email == user.email &&
                                     u.password == SessionHelper.HashPassword(user.password));

            if (user_exist == null)
            {

                return Unauthorized(new { message = "Invalid credentials" });
            }
            var token = JwtHelper.GenerateToken(user_exist, _config); // _config is IConfiguration

            return Ok(new
            {
                Message = "Login successful",
                Token = token,
                UserId = user_exist.id,
                Role = user_exist.Role?.name
            });


            {
            }
            ;

         
        }

        



    }
}
