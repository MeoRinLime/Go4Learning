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
        private CourseService courseService;
        public CourseController(CourseContext context, ILogger<CourseController> logger)
        {
            courseService = new CourseService(context);
            _logger = logger;
        }

        [HttpGet]
        
        public Result<List<Course>> getCourseList()
        {

            return courseService.getCourseList(); ;
        }

        [HttpPost("addCourse")]
        public Result<Course> addCorse(Course course)
        {
            return courseService.addCorse(course);
        }

        [HttpPost("addWebSiteList")]
        public Result<string> addWebsiteList([FromBody]List<WebSite> webSites)
        {
            return courseService.addWebsiteList(webSites);
        }
    }
    
}
