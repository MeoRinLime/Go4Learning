using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace back.Modules
{
    public class WebSite // 参考网站的类定义
    {
        public int WebSiteId { get; set; }
        public string WebSiteName { get; set; }
        public int WebSiteType { get; set; }
        public string WebSiteUrl { get; set; }
        public int CourseId { get; set; }
        public WebSite()
        {

        }
        public WebSite(int id, string name, int type, string url, int courseId)
        {
            WebSiteId = id;
            WebSiteName = name;
            WebSiteType = type;
            WebSiteUrl = url;
            CourseId = courseId;
        }
    }
}

