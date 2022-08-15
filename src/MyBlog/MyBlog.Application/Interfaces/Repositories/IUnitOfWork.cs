using MyBlog.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity:class;
        Task RollBack();
        Task<int> Commit(CancellationToken cancellationToken = default);
        Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
        void Dispose();
    }
      
    
}
