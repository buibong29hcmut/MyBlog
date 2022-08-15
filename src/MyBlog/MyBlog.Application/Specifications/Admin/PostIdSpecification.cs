using MyBlog.Application.Specifications.Base;
using MyBlog.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Specifications.Admin
{
    public class PostIdSpecification: BaseSpecification<Post>
    {
        public PostIdSpecification(Guid Id) : base(p => p.Id == Id)
        {

        }
    }
}
