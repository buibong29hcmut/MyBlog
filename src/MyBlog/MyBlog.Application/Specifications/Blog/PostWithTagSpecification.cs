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
    public class PostWithTagSpecification:BaseSpecification<Post>
    {
        public PostWithTagSpecification(string searchString)
            : base(p => p.Header.Contains(searchString))
        {
            AddInclude(b=>b.Include(p=>p.PostTags).ThenInclude(p=>p.Tag));
            AddOrderByDescending(p => p.Published);


        }
        public PostWithTagSpecification()
          : base()
        {
            AddInclude(b => b.Include(p => p.PostTags).ThenInclude(p => p.Tag));
            AddOrderByDescending(p => p.Published);


        }

        public PostWithTagSpecification(Guid Id)
       : base(p=>p.Id==Id)
        {
            AddInclude(b => b.Include(p => p.PostTags).ThenInclude(p => p.Tag));


        }

    }
}
