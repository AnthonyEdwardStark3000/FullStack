using blog_backend.Models;

namespace blog_backend.Repository.Interface{
    public interface IUserRepository{
        int Add(User user);
        List<User> GetAllUsers();
    }
}