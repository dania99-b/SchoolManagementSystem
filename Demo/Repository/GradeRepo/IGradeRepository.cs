using Demo.DTOs;
using Demo.Models;

namespace Demo.Repository.GradeRepo
{
    public interface IGradeRepository
    {

        public List<Grade> GetAllGrade();
        public Grade create(SubmitAssignmentDTO submitAssignment,int student_id);
        public void update( SubmitAssignmentDTO grade);
        public Grade Get(int id);



    }
}
