using MyBlog.Application.Specifications.Base;
using MyBlog.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Specifications.Admin
{
    public class TagFillterSpecificationAdmin:BaseSpecification<Tag>
    {
        public TagFillterSpecificationAdmin(string searchString) 
            : base(p=>p.Name.Contains(searchString))
        {

        }
    }
}
