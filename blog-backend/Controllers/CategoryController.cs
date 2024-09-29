using Microsoft.AspNetCore.Mvc;
using blog_backend.Repository.Interface;
using blog_backend.Models;
using NLog;
namespace blog_backend.Controllers{
    public class CategoryController:Controller{
                private static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly IUserRepository _userRepository;
        public CategoryController(IUserRepository userRepository){
            _userRepository = userRepository;
        }
        [HttpGet("/getCategories")]
        public ActionResult GetAllCategory(){
            logger.Info("chekc");
            var result = _userRepository.GetAllCategories();
            return Ok(result);
        }
    }
}