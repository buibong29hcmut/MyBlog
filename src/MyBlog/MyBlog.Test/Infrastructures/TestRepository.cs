using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Test.Infrastructures
{
    
    public class TestRepository
    {
        private CreateDbContextInMemory Factory = new CreateDbContextInMemory();

       
        public void TestAddTag()
        {
            Factory._DB.Add(new Tag(".NET"));
            Factory._DB.SaveChanges();
            int count = Factory._DB.Tags.Count();
            Assert.AreEqual(3, count,"Not Equals");
        }
     
    }
}
