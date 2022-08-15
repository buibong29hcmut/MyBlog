
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Domain.Entities.Identity;
using MyBlog.Infrastructure.Configuration.Admin;
using MyBlog.Infrastructure.Configuration.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Infrastructure.Cọntexts
{
    public class MyBlogDbContext:IdentityDbContext<User>
    {
        public MyBlogDbContext(DbContextOptions<MyBlogDbContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogFile> Files { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<BLogCache> BlogCaches { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogTagConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TagConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogCacheConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly);
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);



        }
    }
}
