using Microsoft.AspNetCore.Mvc;
using blog_backend.Repository.Interface;
using blog_backend.Models;
using NLog;
namespace blog_backend.Controllers{
    public class HomeController:Controller{
        private readonly IUserRepository _userRepository;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public HomeController(IUserRepository userRepository){
            _userRepository = userRepository;
        }
        [HttpPost("/create")]
        public ActionResult Create([FromBody]User user){
            var result = _userRepository.Add(user);
            return Ok(result);
        }
        [HttpGet("/getUsers")]
        public ActionResult GetUsers(){
            var result = _userRepository.GetAllUsers();
            return Ok(result);
        }
    }
}