using Demo.DTOs;
using Demo.Models;

namespace Demo.Repository.AssignmentRepo
{
    public interface IAssignmentRepository
    {

        public Assignment create(AssignmentDTO assignment, int id);
        public Assignment Get(int id);
    }
}
