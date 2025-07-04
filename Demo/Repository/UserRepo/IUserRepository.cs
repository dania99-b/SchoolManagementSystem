using Demo.Models;

namespace Demo.Repository.UserRepo
{
    public interface IUserRepository
    {
        public void create(User user);
        public List<User> GetAllUser();
    }
}
