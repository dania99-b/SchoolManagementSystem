using Demo.Models;

namespace Demo.Repository.EnrollmentRepository
{
    public interface IEnrollmentRepository
    {
        public List<Enrollment> GetAllEnrollments();

    }
}
