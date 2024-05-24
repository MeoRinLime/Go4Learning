using System.Collections.Generic;
using back.Context;
using back.Modules;
using Microsoft.EntityFrameworkCore;

namespace back.Service
{
    public class CourseService
    {
        private CourseContext context;
       public  CourseService(CourseContext context) {
        
            this.context = context;
        }

        public Result<List<Course>> getCourseList()
        {
            //try
            {
                var query = context.course;
                List<Course> result = query.ToList();
                if (result.Count <= 0)
                {
                    return Result<List<Course>>.error("当前没有课程");
                }
                result.ForEach(course =>
                {
                    course.KnowledgePoints = context.knowledgePoints.Where(k => k.CourseId == course.CourseId).ToList();
                    course.ReferenceSites = context.webSites.Where(w => w.CourseId == course.CourseId && w.WebSiteType == 0).ToList();
                    course.RecommendedCourses = context.webSites.Where(w => w.CourseId == course.CourseId && w.WebSiteType == 0).ToList();
                });
                return Result<List<Course>>.success(result);
            }
          // catch (Exception ex)
           // {
               // Console.Write(ex.Message);
               // return Result<List<Course>>.error("课程获取失败");
                
           // }
           
        }
    }
}
