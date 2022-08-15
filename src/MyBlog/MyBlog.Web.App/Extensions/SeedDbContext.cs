using MyBlog.Domain.Entities.Blogs;
using MyBlog.Infrastructure.Cọntexts;

namespace MyBlog.Web.App.Extensions
{
    public static class SeedDbContext
    {
        public static async Task SeedData(this MyBlogDbContext _db)
        { string[] TagNames = { ".NET", "Xamarin", "WPF", "MAUI", "ASP.NET", "API", "MATH", "C#", "SQL", "C++" };
            List<Post> PostItems = new List<Post>();
            //List<Tag> Tags= new List<Tag>();
            string content = File.ReadAllText("E:\\Repos\\MyBlog\\src\\MyBlog\\MyBlog.Web.App\\wwwroot\\html\\htmlpage.html");
            Random random = new Random();
            //Tags.Add(new Tag()
            //{   Id=new Guid("aaaa3a05-ab3f-4992-a910-7aa46917fd29"),
            //    Created = DateTimeOffset.UtcNow,
            //    Name = ".NET"
            //});
            //Tags.Add(new Tag()
            //{   Id=new Guid("9a5ee97d-fa48-42fe-96dc-f89ca5fc5cf3"),
            //    Created = DateTimeOffset.UtcNow,
            //    Name = "Xamarin"
            //});;
            //Tags.Add(new Tag()
            //{
            //    Id = new Guid("1f6213e8-2b8c-433a-89fa-6ea84eed184d"),
            //    Created = DateTimeOffset.UtcNow,
            //    Name = "WPF"
            //});
            //Tags.Add(new Tag()
            //{
            //    Id = new Guid("9a5ee98d-fa48-42fe-96dc-f89ca4fc5cf2" +
            //    ""),
            //    Created = DateTimeOffset.UtcNow,
            //    Name = "ASP.NET"
            //});
            //Tags.Add(new Tag()
            //{
            //    Id = new Guid("f7aa71e2-80c2-4da0-9b83-e59bd72956b2"),
            //    Created = DateTimeOffset.UtcNow,
            //    Name = "API"
            //});
            //Tags.Add(new Tag()
            //{
            //    Id = new Guid("9a5ee98d-fa48-42fe-96dc-f79ca5fc5cf3"),
            //    Created = DateTimeOffset.UtcNow,
            //    Name = "MATH"
            //});
            //Tags.Add(new Tag()
            //{
            //    Id = new Guid("f7aa71e2-80c2-4da0-9b83-e59bd72956a2"),
            //    Created = DateTimeOffset.UtcNow,
            //    Name = "C#",
            //});
            //Tags.Add(new Tag()
            //{
            //    Id = new Guid("9a3ee98d-fa48-42fe-96dc-f89ca5fc5cf3"),
            //    Created = DateTimeOffset.UtcNow,
            //    Name = "SQL",
            //});
            //Tags.Add(new Tag()
            //{
            //    Id = new Guid("9a5ee98d-fa48-42fe-96dc-f89caffc4cf3"),
            //    Created = DateTimeOffset.UtcNow,
            //    Name = "C++"
            //});
            //await _db.AddRangeAsync(Tags);
            //await _db.SaveChangesAsync();


            var posts = _db.Posts.ToList();
            var postag = _db.PostTags.ToList();
            var tags = _db.Tags.ToList();
            foreach (var item in posts)
            { 
                int lastindex =  new Random().Next(0, tags.Count());
                for(int i = 0; i <= lastindex; i++)
                {
                    _db.PostTags.Add(new PostTag()
                    {
                        Id = Guid.NewGuid(),
                        TagId = tags[i].Id,
                        PostId=item.Id,
                    }) ;
                    await _db.SaveChangesAsync();
                    await Task.Delay(300);
                }
         
               
            }
                _db.PostTags.RemoveRange(postag);
                await _db.SaveChangesAsync();
            

        }
    }
}
