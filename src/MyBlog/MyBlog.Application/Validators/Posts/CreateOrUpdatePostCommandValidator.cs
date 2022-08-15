using FluentValidation;
using MyBlog.Application.Commands.Admin.CreateOrUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Validators.Posts
{
    public class CreateOrUpdatePostCommandValidator : AbstractValidator<CreateOrUpdatePostCommand>
    {
        public CreateOrUpdatePostCommandValidator()
        {
            RuleFor(p => p.Header).NotEmpty().WithMessage("Header is not emty");
            RuleFor(p => p.Content).NotEmpty().WithMessage("Content is not emty");
            RuleFor(p => p.Content).MinimumLength(200).WithMessage("Content at least 200 characters");
            RuleFor(P => P.UrlImage).NotEmpty().WithMessage("Image is not emty");
            RuleFor(p => p.ShortContent).NotEmpty().WithMessage("ShortContent is not emty");
            
        }
    }
}
