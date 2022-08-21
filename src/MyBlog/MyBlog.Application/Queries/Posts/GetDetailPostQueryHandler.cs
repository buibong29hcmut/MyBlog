using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Exceptions;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Posts;
using MyBlog.Application.Reponses.Tags;
using MyBlog.Application.Requests.Posts;
using MyBlog.Application.Specifications.Admin;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Queries.Posts
{
    public class GetDetailPostQueryHandler:IQueryHandler<GetPostDetailByNameQuery,Result<PostDetail>>
    {
        private readonly IUnitOfWork _unit;
        public GetDetailPostQueryHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }   
        public async Task<Result<PostDetail>> Handle(GetPostDetailByNameQuery query, CancellationToken cancellationToken)
        {
            PostNameSpecification spe = new PostNameSpecification(query.HeaderLink);
            Expression<Func<Post, PostDetail>> expression = p => new PostDetail
            {   Id=p.Id,
                Updated=p.Updated,
                Header = p.Header,
                Published = p.Published,
                Created=p.Created,
                Content=p.Content,
                Tags = p.PostTags.Select(p => new TagForResponse(p.Tag.Name, p.Tag.TagLink))

            };
            var post=  await _unit
                .Repository<Post>()
                .Entities
                .AsNoTracking()
                .Where(spe.Criteria)
                .Select(expression)
                .FirstOrDefaultAsync();
       
            return Result<PostDetail>.Success(post);
           
        }
    }
}
