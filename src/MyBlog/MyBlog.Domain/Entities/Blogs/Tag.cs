using MyBlog.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Entities.Blogs
{
    public class Tag:Entity
    { 
       public Tag()
        {

        }
        public Tag(string name)
        {
            Name = name.Trim().Replace("\n","");
        }

        public ICollection<PostTag> PostTags { get; set; }

        public string Name { get;  set; }
        public string TagLink { get; set; }
      
    }
}
