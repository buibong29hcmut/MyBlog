using MediatR;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Commands.Posts.Delete
{
    public class DeletePostCommand : ICommand<Result<Unit>>
    {
        public Guid PostId { get; private set; }
        public DeletePostCommand(Guid postId)
        {
            PostId = postId;
        }
    }
}
