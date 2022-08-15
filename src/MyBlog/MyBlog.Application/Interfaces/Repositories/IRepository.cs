using MyBlog.Application.Specifications.Base;
using MyBlog.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T:class
    {
        IQueryable<T> Entities { get; }
        IQueryable<T> GetAll(bool trackChanges);
        IQueryable<T> GetAll(ISpecification<T> spec,bool trackChanges);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task RemoveRangeAsync(List<T> entities);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        Task UpdateAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
    }
}
