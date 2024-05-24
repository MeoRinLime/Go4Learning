using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace back.Modules
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime CreateTime { get; set; }
        public int CourseCreatorId { get; set; }
        [NotMapped]
        public List<WebSite> ReferenceSites { get; set; }
        [NotMapped]
        public List<WebSite> RecommendedCourses { get; set; }
        public List<KnowledgePoint> KnowledgePoints { get; set; }

        public Course()
        {
            ReferenceSites = new List<WebSite>();
            RecommendedCourses = new List<WebSite>();
            KnowledgePoints = new List<KnowledgePoint>();
        }

        public void AddReferenceSite(User user, WebSite site)
        {
            if (user.UserType == 1) // 只有老师可以添加
            {
                ReferenceSites.Add(site);
            }
            else
            {
                throw new UnauthorizedAccessException("只有老师可以添加参考网站。");
            }
        }

        public void RemoveReferenceSite(User user, WebSite site)
        {
            if (user.UserType == 1) // 只有老师可以删除
            {
                ReferenceSites.Remove(site);
            }
            else
            {
                throw new UnauthorizedAccessException("只有老师可以删除参考网站。");
            }
        }

        public void AddRecommendedCourse(User user, WebSite course)
        {
            if (user.UserType == 1) // 只有老师可以添加
            {
                RecommendedCourses.Add(course);
            }
            else
            {
                throw new UnauthorizedAccessException("只有老师可以添加推荐课程。");
            }
        }

        public void RemoveRecommendedCourse(User user, WebSite course)
        {
            if (user.UserType == 1) // 只有老师可以删除
            {
                RecommendedCourses.Remove(course);
            }
            else
            {
                throw new UnauthorizedAccessException("只有老师可以删除推荐课程。");
            }
        }

        public void AddKnowledgePoint(User user, KnowledgePoint point)
        {
            if (user.UserType == 1) // 只有老师可以添加
            {
                KnowledgePoints.Add(point);
            }
            else
            {
                throw new UnauthorizedAccessException("只有老师可以添加知识点。");
            }
        }

        public void RemoveKnowledgePoint(User user, KnowledgePoint point)
        {
            if (user.UserType == 1) // 只有老师可以删除
            {
                KnowledgePoints.Remove(point);
            }
            else
            {
                throw new UnauthorizedAccessException("只有老师可以删除知识点。");
            }
        }

        public void UpdateKnowledgePoint(User user, KnowledgePoint point)
        {
            if (user.UserType == 1) // 只有老师可以更新
            {
                var existingPoint = KnowledgePoints.Find(k => k.KnowledgeId == point.KnowledgeId);
                if (existingPoint != null)
                {
                    existingPoint.KnowledgePointContent = point.KnowledgePointContent;
                    existingPoint.KnowledgePointName = point.KnowledgePointName;
                    existingPoint.CourseId = point.CourseId;
                }
            }
            else
            {
                throw new UnauthorizedAccessException("只有老师可以更新知识点。");
            }
        }
    }
}
