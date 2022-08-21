using Microsoft.EntityFrameworkCore;
using MyBlog.Infrastructure.Cọntexts;
try
{


    string conectionString1 = "Server=Desktop-9LLTJF6;Database=BlogDb;Integrated Security=True";
    string conectionString2 = "Server=tcp:blogsever.database.windows.net,1433;Initial Catalog=BlogDb;Persist Security Info=False;User ID=buibong2912;Password=29122002Az@;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    var optionsBuilder1 = new DbContextOptionsBuilder<MyBlogDbContext>();
    optionsBuilder1.UseSqlServer(conectionString1);

    var optionsBuilder2 = new DbContextOptionsBuilder<MyBlogDbContext>();
    optionsBuilder2.UseSqlServer(conectionString2);
    var intanceDb1 = new MyBlogDbContext(optionsBuilder1.Options);
    var intanceDb2 = new MyBlogDbContext(optionsBuilder2.Options);
    /// update post
    //var allposts = intanceDb1.Posts.ToList();
    //await Task.Delay(300);
    //var allposts2 = intanceDb2.Posts.ToList();
    //// intanceDb2.Posts.RemoveRange(allposts2);
    ////await intanceDb2.SaveChangesAsync();
    //// Console.WriteLine("Done");
    //await Task.Delay(300);
    //await intanceDb2.Posts.AddRangeAsync(allposts);
    //await intanceDb2.SaveChangesAsync();
    //await Task.Delay(300);
    //Console.WriteLine("Đã add all post");
    ////foreach (var item in allposts)
    ////{
    ////    await intanceDb2.Posts.AddRangeAsync(allposts);
    ////    await intanceDb2.SaveChangesAsync();
    ////    Console.WriteLine($"Đã Add Post {item.Id}");

    ////}
    //await Task.Delay(300);
    //var tags = intanceDb1.Tags.ToList();
    //await intanceDb2.Tags.AddRangeAsync(tags);
    //Console.WriteLine("đã add all tag");
    ////foreach (var item in tags)
    //{
    //    await intanceDb2.Tags.AddAsync(item);
    //    await intanceDb2.SaveChangesAsync();
    //    Console.WriteLine($"Đã Add Tag {item.Id}");
    //    await Task.Delay(300);
    //}
    ////
    await Task.Delay(300);
    var posttag = intanceDb1.PostTags.ToList();
  await  intanceDb2.AddRangeAsync(posttag);
    await intanceDb2.SaveChangesAsync();
    Console.WriteLine("đã add all posttag");

    //foreach (var item in posttag)
    //{
    //    await intanceDb2.PostTags.AddAsync(item);
    //    await intanceDb2.SaveChangesAsync();
    //    Console.WriteLine($"Đã Add Postag {item.Id}");
    //    await Task.Delay(300);
    //}
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}