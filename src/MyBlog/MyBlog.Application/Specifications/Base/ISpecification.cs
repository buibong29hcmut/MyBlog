using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Specifications.Base
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
         List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; } 
        List<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get;   }
        Expression<Func<T, object>> OrderByDescending { get; }
    }
}
