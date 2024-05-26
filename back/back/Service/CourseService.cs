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

        public Result<List<Course>> getCourseList(int id)
        {
            try
            {
                List<Course> result;
                if (id == -1)
                    result = courseContext.course.Where(c=>c.CourseState==1).ToList();
                else
                    result = courseContext.course.Where(c => c.CourseCreatorId==id).ToList();
                
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

        public Result<List<WebSite> > addWebsiteList(List<WebSite> webSites)
        {
            try
            {
                courseContext.webSites.AddRange(webSites);
                courseContext.SaveChanges();
                return Result< List < WebSite> >.success(webSites);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<List<WebSite>>.error("服务器出错");
            }
        }

        public Result<List<KnowledgePoint>> addKnowledgePointList(List<KnowledgePoint> knowledgePoints)
        {
            try
            {
                courseContext.knowledgePoints.AddRange(knowledgePoints);
                courseContext.SaveChanges();
                return Result<List<KnowledgePoint>>.success(knowledgePoints);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<List<KnowledgePoint>>.error("服务器出错");
            }
        }

        public Result<string> removeCourse(int id)
        {
            try
            {
                var ks= courseContext.knowledgePoints.Where(k => k.CourseId == id);
                courseContext.RemoveRange(ks);
                var ws = courseContext.webSites.Where(w => w.CourseId == id);
                courseContext.RemoveRange(ws);
                Course? c= courseContext.course.FirstOrDefault(k=>k.CourseId == id);
                if(c != null) 
                courseContext.course.Remove(c);
                courseContext.SaveChanges();
                return Result<string>.success("删除成功");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<string>.error("服务器出错");
            }
        }

        public Result<string> removeWebsiteList(List<WebSite> webSites)
        {
            try
            {

                courseContext.RemoveRange(webSites);
                courseContext.SaveChanges();
                return Result<string>.success("删除成功");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<string>.error("服务器出错");
            }
        }

        public Result<string> removeKnowledgePointList(List<KnowledgePoint> knowledgePoints)
        {
            try
            {

                courseContext.RemoveRange(knowledgePoints);
                courseContext.SaveChanges();
                return Result<string>.success("删除成功");
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<string>.error("服务器出错");
            }
        }

        public Result<List<WebSite>> updateWebsiteList(List<WebSite> webSites)
        {
            try
            {

                courseContext.UpdateRange(webSites);
                courseContext.SaveChanges();
                return Result< List<WebSite>>.success(webSites);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<List<WebSite>>.error("服务器出错");
            }
        }

        public Result<List<KnowledgePoint>> updateKnowledgePointList(List<KnowledgePoint> knowledgePoints)
        {
            try
            {
                courseContext.UpdateRange(knowledgePoints);
                courseContext.SaveChanges();
                return Result<List<KnowledgePoint>>.success(knowledgePoints);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<List<KnowledgePoint>>.error("服务器出错");
            }
        }

        public Result<Course> updateCourse(Course course)
        {
            try
            {
                courseContext.Update(course);
                courseContext.SaveChanges();
                return Result<Course>.success(course);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return Result<Course>.error("服务器出错");
            }
        }
    }
}
