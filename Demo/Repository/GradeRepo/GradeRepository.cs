using Demo.context;
using Demo.DTOs;
using Demo.Models;

namespace Demo.Repository.GradeRepo
{
    public class GradeRepository : IGradeRepository
    {
        private readonly MyContext _context;
        public GradeRepository(MyContext myContext)
        {
            this._context = myContext;
        }

        public List<Grade> GetAllGrade()
        {
 
            List<Grade> Allgrades = (from contextobj in _context.Grades select contextobj).ToList();
            return Allgrades;
        
    }

        public Grade create(SubmitAssignmentDTO submission,int student_id)
        {

            var new_submission = new Grade
            {
                student_id = student_id,
                assignment_id = submission.AssignmentId,
                grade = -1
            };
            _context.Grades.Add(new_submission);
            _context.SaveChanges();
            return new_submission;
        }

        public Grade Get(int id)
        {
            Grade grade = (from contextobj in _context.Grades where contextobj.id == id select contextobj).FirstOrDefault();
            return grade;
        }

        public void update(SubmitAssignmentDTO grade)
        {

            Grade existSubmission = _context.Grades
                .FirstOrDefault(c => c.id == grade.id);

            if (existSubmission == null)
            {
                System.Diagnostics.Debug.WriteLine($"❌ grade with ID {grade.id} not found!");
                throw new ArgumentException($"grade with ID {grade.id} not found");
            }
            if (grade.grade != null)
                existSubmission.grade = (int)grade.grade;
          

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
    }
}
