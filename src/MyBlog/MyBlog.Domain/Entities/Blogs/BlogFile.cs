using MyBlog.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Entities.Blogs
{
    public class BlogFile:Entity
    {
        public Guid PostId { get; set; }
        public string UrlFile { get; set; }
        public string FileName { get; set; }
        public Post Post { get; set; }
    }
}
