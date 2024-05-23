using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStructure
{
    public class KnowledgePoint // 知识点类，包含知识点名称和内容部分
    {
        public int KnowledgeId { get; set; }
        public string KnowledgePointContent { get; set; }
        public string KnowledgePointName { get; set; }
        public int CourseId { get; set; }

        public KnowledgePoint() { }

        public KnowledgePoint(int knowledgeId, string content, string name, int courseId)
        {
            KnowledgeId = knowledgeId;
            KnowledgePointContent = content;
            KnowledgePointName = name;
            CourseId = courseId;
        }
    }
}

