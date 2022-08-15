using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Extensions;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Posts;
using MyBlog.Application.Reponses.Tags;
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

namespace MyBlog.Application.Queries.Posts
{
    public class GetAllPostByTagNameHandler : IQueryHandler<GetAllPostByTagQuery, Result<PageList<PostForGetAllByTagName>>>
    {
        private readonly IUnitOfWork _unit;
        public GetAllPostByTagNameHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Result<PageList<PostForGetAllByTagName>>> Handle(GetAllPostByTagQuery getAll, CancellationToken cancellationToken)
        {
         
            Expression<Func<Post, PostForGetAllByTagName>> expression = p => new PostForGetAllByTagName()
            {  Id=p.Id,
                Created = p.Created,
                Header = p.Header,
                ImagePost = p.ImagePost,
                Published = p.Published,
                ShortContent = p.ShortContent,
                HeaderLink=p.HeaderLink,
                IsPublished=p.Published!=null,
                Tags = p.PostTags.Select(p => new TagForResponse(p.Tag.Name,p.Tag.TagLink))
            };
            PostWithTagSpecification postWithTagSpec = new PostWithTagSpecification();
            PostTagWithTagSpecification postTagSpec = new PostTagWithTagSpecification(getAll.TagName);
         
            var result = await (from p in _unit.Repository<Post>().GetAll(postWithTagSpec, false)
                   .QueryByStatus(getAll.Status)
                                join t
                                in _unit.Repository<PostTag>()
                                .GetAll(postTagSpec, false)          
                                on p.Id equals t.PostId
                                select p)
              
             .Select(expression)
             .ToPageListAsync(getAll.PageNumer, getAll.PageSize);
            return Result<PageList<PostForGetAllByTagName>>.Success(result);
        }
    }
}
