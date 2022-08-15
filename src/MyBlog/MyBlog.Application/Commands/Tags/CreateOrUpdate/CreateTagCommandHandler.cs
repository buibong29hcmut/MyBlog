using AutoMapper;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Share.Constants.Cache;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Commands.Tags.CreateOrUpdate
{
    public class CreateTagCommandHandler : ICommandHandler<CreateTagCommand, Result<Tag>>
    {
        private readonly IUnitOfWork _unit;

        public CreateTagCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public async Task<Result<Tag>> Handle(CreateTagCommand tagCommand, CancellationToken cancellationToken)
        {
            Tag tag = new Tag()
            {
                Id = Guid.NewGuid(),
                Created = DateTimeOffset.UtcNow,
                Name = tagCommand.Name,
                TagLink=tagCommand.TagLink

            };
            await _unit.Repository<Tag>().AddAsync(tag);
            await _unit.CommitAndRemoveCache(default, ApplicationConstant.cacheTag);
            return Result<Tag>.Success(tag, "Create tag successfully");

        }

    }
}
