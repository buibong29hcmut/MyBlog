using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Specifications.Base;
using MyBlog.Domain.Common;
using MyBlog.Infrastructure.Cọntexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Infrastructure.Repositories
{
    public class Repository<T>:IRepository<T> 
        where T: class
      
    {
        private readonly MyBlogDbContext _db;
        public Repository(MyBlogDbContext db)
        {
            _db = db;
        }
        public IQueryable<T> Entities => _db.Set<T>();

        public  IQueryable<T> GetAll(bool trackChanges)
        {
            return !trackChanges ? _db.Set<T>().AsNoTracking() : _db.Set<T>();

        }
        public IQueryable<T> GetAll(ISpecification<T> spec, bool trackChanges)
        {
            var query = _db.Set<T>().AsQueryable();
            if (trackChanges == true)
            {
                query = query.AsNoTracking();
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            //  var queryableResultWithIncludes = spec.Includes
            //.Aggregate(query,
            //    (current, include) => current.Include(include));
            var queryableResultWithIncludes = spec
               .Includes
               .Aggregate(query, (current, include) => include(current));
            var secondaryResult = spec.IncludeStrings
           .Aggregate(queryableResultWithIncludes,
           (current, include) => current.Include(include));

            return spec.Criteria==null
                      ?secondaryResult:secondaryResult
                            .Where(spec.Criteria);
                            

        }
        public async Task<T> AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            return entity;
        }
        public  Task DeleteAsync(T entity)
        {
             _db.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
        public  Task RemoveRangeAsync(List<T> entities)
        {
            _db.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ? _db.Set<T>().AsNoTracking().Where(expression) : _db.Set<T>().Where(expression);
        }
        public Task UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
 
            return Task.CompletedTask;
        }
        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            _db.Set<T>().AddRangeAsync(entities);
            return Task.CompletedTask;
        }
    }
}
