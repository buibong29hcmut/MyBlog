using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Posts;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Requests.Posts
{
    public class GetPostByIdQuery : IQuery<Result<PostDetail>>
    {
         public Guid IdPost { get; set; }
    }
}
