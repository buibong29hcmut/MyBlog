using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Specifications.Base;
using MyBlog.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Specifications.Blog
{
    public class PostTagWithTagSpecification:BaseSpecification<PostTag>
    {
        public PostTagWithTagSpecification(string TagLink) : base(p => p.Tag.TagLink == TagLink)
        {
            AddInclude(p => p.Include(p => p.Tag));
            
        }
    }
}
