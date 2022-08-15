using Microsoft.EntityFrameworkCore;
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
    internal class GetDetailPostByIdQueryHandler:IQueryHandler<GetPostByIdQuery, Result<PostDetail>>
    {
        private readonly IUnitOfWork _unit;
        public GetDetailPostByIdQueryHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Result<PostDetail>> Handle(GetPostByIdQuery query, CancellationToken cancellationToken)
        {
            PostIdSpecification spe = new PostIdSpecification(query.IdPost);
            Expression<Func<Post, PostDetail>> expression = p => new PostDetail
            {   Id=p.Id,
                Updated=p.Updated,
                Created = p.Created,
                Header = p.Header,
                Published = p.Published,
                ShortContent = p.ShortContent,
                Content = p.Content,
                ImagePost=p.ImagePost,
                Tags = p.PostTags.Select(p => new TagForResponse(p.Tag.Name,p.Tag.TagLink))

            };
            var post = await _unit
                .Repository<Post>()
                .FindByCondition(spe.Criteria, false)
                .Select(expression).FirstOrDefaultAsync();

            return Result<PostDetail>.Success(post);

        }
    }
}
