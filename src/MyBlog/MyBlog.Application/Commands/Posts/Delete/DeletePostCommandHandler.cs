using MediatR;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Application.Exceptions;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Specifications.Admin;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Commands.Posts.Delete
{
    public class DeletePostCommandHandler:ICommandHandler<DeletePostCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unit;
        public DeletePostCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;   
        }
        public async  Task<Result<Unit>> Handle(DeletePostCommand delete,CancellationToken cancellationToken)
        {   PostIdSpecification spe = new PostIdSpecification(delete.PostId);
            var post= _unit.Repository<Post>().FindByCondition(spe.Criteria,true).FirstOrDefault();
            if (post == null)
            {
                throw new BusinessException("Post can't exist or delete");
            }
            await  _unit.Repository<Post>().DeleteAsync(post);
            await _unit.CommitAndRemoveCache(default,post.HeaderLink);
            return Result<Unit>.Success(Unit.Value,$"Đã Xóa post {delete.PostId} thành công");
        }
    }
}
