using MediatR;
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
    public class GetAllPostQueryHandler : IQueryHandler<GetAllPostQuery, Result<PageList<PostForGetAllResponse>>>
    {
        private readonly IUnitOfWork _unit;
        public GetAllPostQueryHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Result<PageList<PostForGetAllResponse>>> Handle(GetAllPostQuery getAllPostQuery, CancellationToken cancellationToken)
        {
            int pageSize = getAllPostQuery.PageSize;
            int pageNumer = getAllPostQuery.PageNumer;
         
            Expression<Func<Post, PostForGetAllResponse>> expression = p => new PostForGetAllResponse
            {   Id=p.Id,
                Created = p.Created,
                Header = p.Header,
                ImagePost = p.ImagePost,
                Published = p.Published,
                HeaderLink=p.HeaderLink,
                ShortContent = p.ShortContent,
                Tags = p.PostTags.Select(p => new TagForResponse(p.Tag.Name,p.Tag.TagLink)),
                IsPublished=p.Published!=null,

            };     
            PostWithTagSpecification postFillter = new PostWithTagSpecification(getAllPostQuery.SearchString);
            var result = await _unit.Repository<Post>().GetAll(postFillter,false)
                      .QueryByStatus(getAllPostQuery.Status)
                      .Select(expression)
                      .ToPageListAsync(getAllPostQuery.PageNumer, getAllPostQuery.PageSize);



            return Result<PageList<PostForGetAllResponse>>.Success(result);

        }
    }
}
