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
        public int? CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime CreateTime { get; set; }
        public int CourseCreatorId { get; set; }
        public int CourseState { get; set; }
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

    }
}
