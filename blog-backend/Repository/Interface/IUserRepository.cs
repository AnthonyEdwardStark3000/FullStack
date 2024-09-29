using blog_backend.Models;

namespace blog_backend.Repository.Interface{
    public interface IUserRepository{
        int Add(User user);
        List<User> GetAllUsers();

        // Blogs
        Task<List<Blog>>GetAllBlogs();
        Task<Blog> GetBlogById(string id);
        int AddBlog(Blog b);
        int UpdateBlog(string id,Blog b);
        void DeleteBlog(string id);
        Task SaveBlogChanges();
        List<Blog> GetFeaturedBlog();
        List<Category>GetAllCategories();
    }
}