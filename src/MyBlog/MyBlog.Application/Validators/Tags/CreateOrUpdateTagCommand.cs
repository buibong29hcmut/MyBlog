using FluentValidation;
using MyBlog.Application.Commands.Admin.CreateOrUpdate;
using MyBlog.Application.Commands.Tags.CreateOrUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Validators.Tags
{
    public class CreateOrUpdateTagValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateOrUpdateTagValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Tag is not emty");
            
        }
    }
}
