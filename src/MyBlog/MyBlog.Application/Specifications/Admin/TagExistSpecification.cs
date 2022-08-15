using MyBlog.Application.Specifications.Base;
using MyBlog.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Specifications.Admin
{
    public class TagExistSpecification:BaseSpecification<Tag>
    {
        public TagExistSpecification(string name) 
            : base(p => p.Name.Equals(name))
        {

        }
    
    }
}
