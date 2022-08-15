using FluentValidation;
using MediatR;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Commands.Admin.CreateOrUpdate
{
    public class CreateOrUpdatePostCommand:ICommand<Result<Unit>>
    {   public CreateOrUpdatePostCommand()
        {

        }
        public CreateOrUpdatePostCommand(Guid id )
        {
            Id = id;    
        }
        public Guid Id { get; set; }
        public string Header { get; set; }
        public string UrlImage { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public List<string> Tags { get; set; }
        public string HeaderLink { get; set; }
        public bool IsVisible { get; set; } = false;
      
    }
 


}
