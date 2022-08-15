using Microsoft.AspNetCore.Components;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Posts;
using MyBlog.Application.Requests.Posts;
using MyBlog.Share.Wrappers;

namespace MyBlog.Web.App.Components
{
    public partial class PostDetailComponent
    {
        [Parameter]
        public PostDetail? Post { get; set; } 
      
    }
}
