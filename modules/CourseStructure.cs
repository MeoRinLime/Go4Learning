using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStructure
{
    // CourseStructure 类，包含所有项目的集合
    public class CourseStructure
    {
        public List<ReferenceSite> ReferenceSites { get; set; }
        public List<RecommendedCourse> RecommendedCourses { get; set; }
        public List<KnowledgePoint> KnowledgePoints { get; set; }

        public CourseStructure()
        {
            ReferenceSites = new List<ReferenceSite>();
            RecommendedCourses = new List<RecommendedCourse>();
            KnowledgePoints = new List<KnowledgePoint>();
        }

        public void AddReferenceSite(ReferenceSite site)
        {
            ReferenceSites.Add(site);
        }

        public void AddRecommendedCourse(RecommendedCourse course)
        {
            RecommendedCourses.Add(course);
        }

        public void AddKnowledgePoint(KnowledgePoint point)
        {
            KnowledgePoints.Add(point);
        }
    }
}
