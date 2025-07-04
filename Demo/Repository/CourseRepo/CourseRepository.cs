
using Demo.context;
using Demo.DTOs;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository.CourseRepo
{
    public class CourseRepository : ICourseRepository
    {
        public readonly MyContext _context;
        public CourseRepository(MyContext context)
        {
            _context = context;
        }
        public Course create(CourseDTO course, int id)
        {

            var new_course = new Course
            {
                user_id = id,
                course_name = course.course_name,
                course_capacity = course.course_capacity
            };
            _context.Courses.Add(new_course);
            _context.SaveChanges();
            return new_course;
        }

        public void delete(int id)
        {
            Course course = (from contextobj in _context.Courses
                             where contextobj.id == id
                             select contextobj).FirstOrDefault();
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }

        public Course Get(int id)
        {
            Course course = (from contextobj in _context.Courses where contextobj.id == id select contextobj).FirstOrDefault();
            return course;
        }

        public List<Course> GetAllCourse()
        {
            List<Course> Allcourses = (from contextobj in _context.Courses select contextobj).ToList();
            return Allcourses;
        }

        public void update(int id, CourseDTO course)
        {
            Console.WriteLine("Attempting to update course in the database.");

            Course existCourse = _context.Courses
                .FirstOrDefault(c => c.id == id);

            if (existCourse == null)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Course with ID {id} not found!");
                throw new ArgumentException($"Course with ID {id} not found");
            }
            if (course.course_capacity != null)
                existCourse.course_capacity = (int)course.course_capacity;
            if (course.course_name != null)
                existCourse.course_name = course.course_name;

            try
            {
                _context.SaveChanges();
                System.Diagnostics.Debug.WriteLine("✔️ Debug: Update successful");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Error saving changes: {ex.Message}");
                throw; // Re-throw to handle in controller
            }
        }

        public Enrollment EnrollStudent(int coursee_id, int student_id)
        {
           
            var new_enrollment = new Enrollment
            {

                course_id = coursee_id,
                user_id = student_id


            };
            _context.Add( new_enrollment );
            _context.SaveChanges ();
            return new_enrollment;


        }

        List<Enrollment> ICourseRepository.getEnrollStudent()
        {
            List<Enrollment> AllEnrollment = (from contextobj in _context.Enrollments select contextobj).ToList();
            return AllEnrollment;
        }

        List<Course> ICourseRepository.GetCoursesByStudentId(int student_id)
        {
            var courses = (from enrollment in _context.Enrollments
                           where enrollment.user_id == student_id
                           join course in _context.Courses
                           on enrollment.course_id equals course.id
                           select course)
                    .ToList();

            return courses;
        }

        
    }
}
