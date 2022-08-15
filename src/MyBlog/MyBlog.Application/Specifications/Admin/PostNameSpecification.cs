using MyBlog.Application.Specifications.Base;
using MyBlog.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Specifications.Admin
{
    public class PostNameSpecification : BaseSpecification<Post>
    {
        public PostNameSpecification(string Name) : base(p => p.HeaderLink == Name)
        {

        }
    }
}
