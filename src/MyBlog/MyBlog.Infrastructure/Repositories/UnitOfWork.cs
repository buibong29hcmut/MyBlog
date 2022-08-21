using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Domain.Common;
using MyBlog.Infrastructure.Cọntexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyBlog.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly IDistributedCache _cache;
        private bool _disposed;
        private Hashtable _repositories;
        private readonly IDbContextFactory<MyBlogDbContext> _factory;
        private  MyBlogDbContext _db { get; }
        public UnitOfWork(IDistributedCache cache, IDbContextFactory<MyBlogDbContext> factory)
        {
            _cache = cache;
            _factory = factory;
            _db = _factory.CreateDbContext();
        }
        public IRepository<TEntity> Repository<TEntity>() where TEntity:class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
               var repositoryType = typeof(Repository<>);

                    var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _db);

                    _repositories.Add(type, repositoryInstance);
                
            
            }

            return (IRepository<TEntity>)_repositories[type];
        }
       public Task RollBack()
        {
        
                _db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

          
            return Task.CompletedTask;
        }
        public async Task<int> Commit(CancellationToken cancellationToken=default)
        {
          
                return await _db.SaveChangesAsync(cancellationToken);

            
        }

        public async Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {   var result= await _db.SaveChangesAsync(cancellationToken);
                foreach (var cacheKey in cacheKeys)
                {
                    _cache.Remove(cacheKey);
                }
                return result;
            
           
       
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                    _db.Dispose();
                }
            }
            
            _disposed = true;
        }
    }
}
