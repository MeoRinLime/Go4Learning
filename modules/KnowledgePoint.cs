using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStructure
{
    public class KnowledgePoint//知识点类，包含知识点名称和内容部分
    {
        public string Name { get; set; }
        public List<ContentSection> ContentSections { get; set; }

        public KnowledgePoint(string name)
        {
            Name = name;
            ContentSections = new List<ContentSection>();
        }

        public void AddContentSection(ContentSection section)
        {
            ContentSections.Add(section);
        }
    }
}
