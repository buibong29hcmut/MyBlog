using Microsoft.EntityFrameworkCore;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Tags;
using MyBlog.Application.Requests.Tags;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Queries.Tags
{
    public class GetAllTagQueryHandler:IQueryHandler<GetAllTagQuery, Result<List<TagForResponse>>>
    {
        private readonly IUnitOfWork _unit;
        public GetAllTagQueryHandler(IUnitOfWork unit)
        {
            _unit = unit;   
        }
        public async Task<Result<List<TagForResponse>>> Handle(GetAllTagQuery query,CancellationToken cancellationToken)
        {
            Expression<Func<Tag, TagForResponse>> expression =p=> new TagForResponse(p.Name, p.TagLink);
            var result =await _unit.Repository<Tag>()
                            .GetAll(false)
                            .Select(expression)
                            .ToListAsync();
           return   Result<List<TagForResponse>>.Success(result);
        }
    }
}
