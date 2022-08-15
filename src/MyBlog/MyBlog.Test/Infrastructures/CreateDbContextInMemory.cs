using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Commands.Admin.CreateOrUpdate;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Infrastructure.Cọntexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Test.Infrastructures
{
    public class CreateDbContextInMemory
    {
        public MyBlogDbContext _DB { get;  }
        public CreateDbContextInMemory()
        {
            var myDatabaseName = "sqlsever" + DateTime.Now.ToFileTimeUtc();

            var options = new DbContextOptionsBuilder<MyBlogDbContext>()
                            .UseInMemoryDatabase(databaseName: myDatabaseName)
                            .EnableSensitiveDataLogging()
                            .Options;
            _DB = new MyBlogDbContext(options);
            _DB.AddRange(new List<Tag>()
            {
                new Tag(".NET")
                {
                    Id=new Guid("b536176b-3722-4795-a6fb-64de7957094c"),
                },
                new Tag(".Xamarin"),
                new Tag("WPF"),
            });
        
            _DB.AddRange(new List<PostTag>()
            {
                new PostTag()
                {
                    PostId=new Guid("59750f63-c537-441d-9b75-d60af9eb602c"),
                    TagId=new Guid("b536176b-3722-4795-a6fb-64de7957094c"),
                }
            });

            _DB.AddRange(new List<Post>()
            {
                new Post()
                {
                  Id = new Guid("bef14b32-1f92-44e6-aa38-ae58e5c31455"),
                 Header = "A",
                 Content = "A",
                 ShortContent = "A",
                },
                new Post()
                {
                    Id=Guid.NewGuid(),
                },
                new Post()
                {
                    Id=Guid.NewGuid(),
                }
            });
            _DB.SaveChanges();


           
        }
        
    }
}
