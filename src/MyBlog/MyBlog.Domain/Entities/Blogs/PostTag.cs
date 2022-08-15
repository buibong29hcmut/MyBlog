using MyBlog.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Entities.Blogs
{
    public class PostTag:Entity
    {   

        public  Guid PostId { get; set; }
        public  Guid TagId { get; set; }
        public Tag Tag { get; set; }
        public Post Post { get; set; }
        public static PostTag Create(Tag tag,Post post)
        {
            return new PostTag()
            {
                PostId = post.Id,
                Tag=tag,
                Created=DateTime.Now,   
            };
        }
        public static PostTag Create(Guid tagId, Guid postID)
        {
            return new PostTag()
            {
                PostId = postID,
                TagId = tagId,
                Created = DateTime.Now,
            };
        }



    }
}
