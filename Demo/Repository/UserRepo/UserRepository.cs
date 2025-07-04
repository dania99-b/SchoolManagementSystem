using Demo.context;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly MyContext _context;
        public UserRepository(MyContext context) {  _context = context; }
        public void create(User user)
        {
         
            _context.Users.Add(user);
            _context.SaveChanges();
        
    }

        public List<User> GetAllUser()
        {
            List<User> allUsers = _context.Users
                                                .Include(s => s.Role) // 👈 This includes the Role object
                                                .ToList();
            return allUsers;
        }
    }
}
