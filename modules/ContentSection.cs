using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseStructure
{
    // 内容部分类，表示知识点页面的一个部分
    public class ContentSection
    {
        public string Title { get; set; } // 标题
        public ContentType Type { get; set; } // 内容类型
        public string Content { get; set; } // 内容
        public List<ContentSection> SubSections { get; set; } // 子部分
        public ContentSection ParentSection { get; set; } // 父部分
        public int Order { get; set; } // 子部分的顺序

        public ContentSection(string title, ContentType type, string content, int order = 0)
        {
            Title = title;
            Type = type;
            Content = content;
            Order = order;
            SubSections = new List<ContentSection>();
        }

        public void AddSubSection(ContentSection section)
        {
            section.ParentSection = this;
            section.Order = SubSections.Count + 1;
            SubSections.Add(section);
        }
    }
}
