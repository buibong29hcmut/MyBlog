using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Infrastructure.Configuration.Blog
{
    public class BlogTagConfiguration: IEntityTypeConfiguration<PostTag>
    {

        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
          
            
        }
    }
}
