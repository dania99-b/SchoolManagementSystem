using Demo.context;
using Demo.Models;

namespace Demo.Repository.EnrollmentRepository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        public readonly MyContext _context;
public EnrollmentRepository(MyContext context) {  _context = context; }
        public List<Enrollment> GetAllEnrollments()
        {
            List<Enrollment> AllEnrollment = (from contextobj in _context.Enrollments select contextobj).ToList();
            return AllEnrollment;
        }
    }
}
