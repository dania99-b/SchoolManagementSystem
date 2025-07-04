using Demo.DTOs;
using Demo.Models;

namespace Demo.Repository.CourseRepo
{
    public interface ICourseRepository
    {

        public List<Course> GetAllCourse();
        public Course create(CourseDTO course,int id);
        public void update(int id,CourseDTO course);
        public void delete(int id);
        public Course Get(int id);

        public Enrollment EnrollStudent(int course_id,int student_id);
        public List<Enrollment> getEnrollStudent();
        public List<Course> GetCoursesByStudentId(int student_id);
    }
}
