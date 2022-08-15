using MyBlog.Application.Specifications.Base;
using MyBlog.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Specifications.Blog
{
    public class PostPublishedSpecification:BaseSpecification<Post>
    {
        public PostPublishedSpecification() : base(p => p.Published != null)
        {

        }
    }
}
