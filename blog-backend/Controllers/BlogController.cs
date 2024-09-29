using Microsoft.AspNetCore.Mvc;
using blog_backend.Repository.Interface;
using blog_backend.Models;
using NLog;
namespace blog_backend.Controllers{
    public class BlogController:Controller{
        private readonly IUserRepository _userRepository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public BlogController(IUserRepository userRepository){
            _userRepository = userRepository;
        }
        
        [HttpGet("/getAllBlogs")]
        public ActionResult GetAllBlogs(){
            var result = _userRepository.GetAllBlogs();
            return Ok(result);
        }
        [HttpGet("/getSingleBlog/{id}")]
        public ActionResult GetBlogById([FromRoute]string id){
            var result = _userRepository.GetBlogById(id);
            return Ok(result);
        }
        [HttpGet("/getFeaturedBlogs")]
        public ActionResult GetFeaturedBlog(){
            var result = _userRepository.GetFeaturedBlog();
            return Ok(result);
        }
        [HttpPut("/updateBlog/{id}")]
        public ActionResult UpdateBlog([FromRoute]string id,[FromBody] Blog b){
            var result = _userRepository.UpdateBlog(id,b);
            return Ok(result);
        }
        [HttpPost("/createNewBlog")]
        public ActionResult CreateNewBlog([FromBody]Blog blog){
            var result = _userRepository.AddBlog(blog);
            return Ok(result);
        }
        [HttpDelete("/deleteBlog/{id}")]
        public ActionResult DeleteBlog(string id){
            try{
            _userRepository.DeleteBlog(id);
            return Ok("Row deleted successfully");
            }catch(Exception ex){
            return StatusCode(500,$"Row deleted failed with an exception {ex}");
            }
        }
    }
}