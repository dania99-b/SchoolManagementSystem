using Demo.context;
using Demo.DTOs;
using Demo.Models;

namespace Demo.Repository.AssignmentRepo
{
    public class AssignmentRepository:IAssignmentRepository
    {

        public readonly MyContext _context;
        public AssignmentRepository(MyContext context) {  _context = context; }

        Assignment IAssignmentRepository.create(AssignmentDTO assignment, int id)
        {

            var new_assignment = new Assignment
            {
                assignment_name = assignment.assignment_name,
                course_id = assignment.course_id,
                description = assignment.description,
                DueDate= (DateTime)assignment.due_date,
                teacher_id= id
            };
            _context.Assignments.Add(new_assignment);
            _context.SaveChanges();
            return new_assignment;
        }

        public Assignment Get(int id)
        {
            Assignment assignment = (from contextobj in _context.Assignments where contextobj.id == id select contextobj).FirstOrDefault();
            return assignment;
        }
    }
}
