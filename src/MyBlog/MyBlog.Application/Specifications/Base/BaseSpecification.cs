using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Specifications.Base
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public BaseSpecification()
        {
            
        }
        public Expression<Func<T, bool>> Criteria { get; }

        //public List<Expression<Func<T, object>>> Includes { get; } =
        //                                       new List<Expression<Func<T, object>>>();
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; } = new();

        public Expression<Func<T, object>> OrderBy { get; private set; }
           public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public List<string> IncludeStrings { get; } = new List<string>();

        //protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        //{
        //    Includes.Add(includeExpression);
        //}
        protected virtual void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}
