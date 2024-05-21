using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStructure
{
    // 推荐课程类，包含课程名称和网址
    public class RecommendedCourse
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public RecommendedCourse(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
