using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Test.Infrastructures
{
    public class CreateDatabaseCache
    {
        public IServiceCollection ServiceCollection { get; private set; }
        private static string Sever = "Server=Desktop-9LLTJF6;Database=BlogDb;Integrated Security=True";
        public CreateDatabaseCache()
        {
            CreateDatabaseCacheIntance();
        }
        public void CreateDatabaseCacheIntance()
        {
            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = Sever;
                options.SchemaName = "dbo";
                options.TableName = "BlogCaches";
            });
        }
    }
}
