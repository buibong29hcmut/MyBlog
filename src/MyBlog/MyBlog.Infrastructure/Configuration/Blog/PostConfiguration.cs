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
    public class PostConfiguration: IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(p => p.PostTags).WithOne(p => p.Post).HasForeignKey(p => p.PostId);
            builder.HasIndex(p => p.HeaderLink);
            builder.Property(p => p.ImagePost).HasMaxLength(300);
            builder.Property(p => p.Header).HasMaxLength(300);
            builder.Property(p => p.HeaderLink).HasMaxLength(300);
         

        }
    }
}
