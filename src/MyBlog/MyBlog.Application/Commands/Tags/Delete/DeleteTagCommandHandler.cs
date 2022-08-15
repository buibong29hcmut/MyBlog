using AutoMapper;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Commands.Tags.Delete
{
    public class DeleteTagCommandHandler : ICommandHandler<DeleteTagCommand, Result<int>>
    {
        private readonly IUnitOfWork _unit;
        public DeleteTagCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Result<int>> Handle(DeleteTagCommand deleteTag, CancellationToken cancellationToken)
        {
            Tag tag = new Tag()
            {
                Id = deleteTag.Id,
            };
            await _unit.Repository<Tag>().DeleteAsync(tag);
            return Result<int>.Success(await _unit.Commit());
        }
    }
}
