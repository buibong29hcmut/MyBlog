using FluentValidation;
using MyBlog.Application.Commands.Admin.CreateOrUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Validators.Posts
{
    public class DeletePostCommandValidator : AbstractValidator<CreateOrUpdatePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
