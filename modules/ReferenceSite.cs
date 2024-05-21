using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStructure { 
        public class ReferenceSite//参考网站的类定义
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public ReferenceSite(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
