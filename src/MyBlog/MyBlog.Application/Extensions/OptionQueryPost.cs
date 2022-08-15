using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MyBlog.Application.Enum;
using MyBlog.Application.Requests.Posts;
using MyBlog.Application.Specifications.Blog;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Extensions
{
    public static class OptionQueryPost
    {
        public static IQueryable<Post> QueryByStatus(this IQueryable<Post> query, QueryPostStatus option)
        {
            if (option == Enum.QueryPostStatus.NotPublished)
            {
                PostNotPublisedSpecification notPublisedSpecification = new PostNotPublisedSpecification();
                query = query.Where(notPublisedSpecification.Criteria);


            }
            else if (option == Enum.QueryPostStatus.Published)
            {
                PostPublishedSpecification publishedSpecification = new PostPublishedSpecification();
                query = query.Where(publishedSpecification.Criteria);


            }
            return query;
        }
      
    }
}
