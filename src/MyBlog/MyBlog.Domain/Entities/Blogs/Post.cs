using MyBlog.Domain.Common;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Entities.Blogs
{
    public class Post:Entity
    {   
        public string Header { get; set; }
        public string HeaderLink { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public string ImagePost { get; set; }
        public DateTimeOffset ?Published { get; set; }
        public DateTimeOffset Updated { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
        
    }
}
