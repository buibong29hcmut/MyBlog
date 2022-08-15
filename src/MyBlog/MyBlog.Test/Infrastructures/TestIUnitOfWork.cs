using MediatR;
using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Commands.Admin.CreateOrUpdate;
using MyBlog.Application.Queries.Posts;
using MyBlog.Application.Requests.Posts;
using MyBlog.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Test.Infrastructures
{
    [TestClass]   
    public class TestIUnitOfWork
    {
        private readonly CreateIUnitOfWorkByDbContextInmemory _create;
        public TestIUnitOfWork()
        {
            CreateDbContextInMemory createDb = new CreateDbContextInMemory();
            _create = new CreateIUnitOfWorkByDbContextInmemory(createDb);
        }   
        [TestMethod]
        public async Task AddTag()
        {
            List<string> tagstringS = new List<string>() { ".NET", "Winform" };
       
            CreateOrUpdatePostCommand post = new CreateOrUpdatePostCommand()
            {
                Id = new Guid("bef14b32-1f92-44e6-aa38-ee58e5c31455"),
                Header = "B",
                Content = "A",
                ShortContent = "A",
                Tags = tagstringS
            };
            CreateOrUpdatePostCommandHandler command = new CreateOrUpdatePostCommandHandler(_create.unit);
         await   command.Handle(post,default(CancellationToken));

            var posts = _create.unit.Repository<Post>().GetAll(false).ToList();

            GetAllPostByTagNameHandler handler = new GetAllPostByTagNameHandler(_create.unit);
            GetAllPostByTagQuery query = new GetAllPostByTagQuery()
            {
                TagName=".NET",
            };
         var result= (await  handler.Handle(query, default)).Data;
            Assert.AreEqual("A", "B", "Equals");


        }
    }
}
