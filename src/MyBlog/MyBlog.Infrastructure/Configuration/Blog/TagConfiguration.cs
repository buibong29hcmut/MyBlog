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
    public class TagConfiguration: IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasMany(p => p.PostTags).WithOne(p => p.Tag).HasForeignKey(p => p.TagId);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.TagLink).HasMaxLength(500);
        }
    }
}
