using FluentValidation;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Domain.Entities.Blogs;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Commands.Tags.CreateOrUpdate
{
    public class CreateTagCommand : ICommand<Result<Tag>>
    {
        public string Name { get; set; }
        public string TagLink { get; set; }
        public CreateTagCommand(string name)
        {
            Name = name;
        }
    }

}
