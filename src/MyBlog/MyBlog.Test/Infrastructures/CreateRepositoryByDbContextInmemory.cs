using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Test.Infrastructures
{
    public class CreateIUnitOfWorkByDbContextInmemory
    {
        public IUnitOfWork unit { get; }
        
        public CreateIUnitOfWorkByDbContextInmemory(CreateDbContextInMemory createDb)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            var provider= services.BuildServiceProvider();
            CreateDatabaseCache createDatabase = new CreateDatabaseCache();
            var service = createDatabase.ServiceCollection;
            var provider2 = service.BuildServiceProvider();
            var distributedCacheService = provider2.GetRequiredService<IDistributedCache>();
            var cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024,
            });
            unit = new UnitOfWork(distributedCacheService, createDb._DB);
        }
       
    }
}
