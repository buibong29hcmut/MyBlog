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
    public class GetPostDetailByNameQuery : IQuery<Result<PostDetail>>
    {
        public string HeaderLink { get; set; }
        public GetPostDetailByNameQuery(string HeaderLink)
        {
           this.HeaderLink = HeaderLink;
        }   
    }
}
