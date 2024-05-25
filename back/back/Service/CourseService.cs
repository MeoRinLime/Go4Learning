using System.Collections.Generic;
using back.Context;
using back.Modules;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;

namespace back.Service
{
    public class CourseService
    {
        private CourseContext courseContext;
       public  CourseService(CourseContext context) {
        
            this.courseContext = context;
        }

        public Result<List<Course>> getCourseList()
        {
            try
            {
                var query = courseContext.course.Where(c=>c.CourseState==1);
                List<Course> result = query.ToList();
                if (result.Count <= 0)
                {
                    return Result<List<Course>>.error("当前没有课程");
                }
                result.ForEach(course =>
                {
                    course.KnowledgePoints = courseContext.knowledgePoints.Where(k => k.CourseId == course.CourseId).ToList();
                    course.ReferenceSites = courseContext.webSites.Where(w => w.CourseId == course.CourseId && w.WebSiteType == 0).ToList();
                    course.RecommendedCourses = courseContext.webSites.Where(w => w.CourseId == course.CourseId && w.WebSiteType == 1).ToList();
                });
                return Result<List<Course>>.success(result);
            }
           catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<List<Course>>.error("课程获取失败");
                
            }
           
        }

        public Result<Course> addCorse(Course course)
        {
            try
            {
                courseContext.Add(course);
                courseContext.SaveChanges();
                return Result<Course>.success(course);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<Course>.error("服务器出错");
            }
           
        }

        public Result<string> addWebsiteList(List<WebSite> webSites)
        {
            try
            {
                courseContext.webSites.AddRange(webSites);
                courseContext.SaveChanges();
                return Result<string>.success("正常");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<string>.error("服务器出错");
            }
        }
    }
}
