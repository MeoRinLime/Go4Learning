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

        [HttpGet("{id}")]
        public Result<List<Course>> getCourseList(int id)
        {

            return courseService.getCourseList(id);
        }

        [HttpPost("addCourse")]
        public Result<Course> addCorse(Course course)
        {
            return courseService.addCorse(course);
        }

        [HttpPost("addWebSiteList")]
        public Result<List<WebSite>> addWebsiteList([FromBody] List<WebSite> webSites)
        {
            return courseService.addWebsiteList(webSites);
        }
        [HttpPost("addKnowledgePointList")]
        public Result<List<KnowledgePoint>> addKnowledgePointList(List<KnowledgePoint> knowledgePoints)
        {
            return courseService.addKnowledgePointList(knowledgePoints);
        }

        [HttpPost("updateWebSiteList")]

        public Result<List<WebSite>> updateWebsiteList([FromBody] List<WebSite> webSites)
        {
            return courseService.updateWebsiteList(webSites);
        }

        [HttpPost("updateKnowledgePointList")]
        public Result<List<KnowledgePoint>> updateKnowledgePointList(List<KnowledgePoint> knowledgePoints)
        {
            return courseService.updateKnowledgePointList(knowledgePoints);
        }
        [HttpPost("updateCourse")]
        public Result<Course> removeCourse(Course course)
        {
            return courseService.updateCourse(course);
        }


        [HttpPost("removeWebSiteList")]
        public Result<string> removeWebsiteList([FromBody] List<WebSite> webSites)
        {

            return courseService.removeWebsiteList(webSites);
        }
        [HttpPost("removeKnowledgePointList")]
        public Result<string> removeKnowledgePointList(List<KnowledgePoint> knowledgePoints)
        {
            return courseService.removeKnowledgePointList(knowledgePoints);
        }
        [HttpGet("removeCourse/{id}")]
        public Result<string> removeCourse(int id) {
            return courseService.removeCourse(id);
        }
        
    }
    
}
