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
    public class BlogCacheConfiguration: IEntityTypeConfiguration<BLogCache>
    {
        public void Configure(EntityTypeBuilder<BLogCache> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasIndex(p => p.Id).IsUnique();
            builder.Property(p => p.Id).HasMaxLength(449);
            
        }
    }
}
