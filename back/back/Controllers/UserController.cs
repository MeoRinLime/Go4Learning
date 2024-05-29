using back.Context;
using back.Modules;
using back.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private UserService userService;
        public UserController(UserContext context, ILogger<CourseController> logger)
        {
            userService = new UserService(context);
            _logger = logger;
        }
        [HttpPost("register")]
        public Result<string> register( User user) {

            return userService.register(user);
        }
        [HttpPost("login")]
        public Result<User> login(User user) {
            return userService.login(user);
        }
    }
}
