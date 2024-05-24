using back.Context;
using back.Modules;
using back.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back.Controllers
{
    
    [ApiController]
    [Route("course")]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;
        private CourseService CourseService;
        public CourseController(CourseContext context, ILogger<CourseController> logger)
        {
            CourseService = new CourseService(context);
            _logger = logger;
        }

        [HttpGet]
        
        public Result<List<Course>> getCourseList()
        {

            return CourseService.getCourseList(); ;
        }
      
    }
    
}
