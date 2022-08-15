using FluentValidation;
using MyBlog.Application.Commands.Tags.CreateOrUpdate;
using MyBlog.Application.Commands.Tags.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Validators.Tags
{
    public class DeleteTagCommandValidator: AbstractValidator<DeleteTagCommand>
    {
        public DeleteTagCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Tag Id must be not emty");
        }
    }
}
