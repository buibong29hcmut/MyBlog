using MyBlog.Application.Commands.Abstracts;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBlog.Application.Commands.Tags.Delete
{
    public class DeleteTagCommand : ICommand<Result<int>>
    {
        public Guid Id { get; set; }
        public DeleteTagCommand(Guid Id)
        {
           this.Id= Id;
        }
    }
}
