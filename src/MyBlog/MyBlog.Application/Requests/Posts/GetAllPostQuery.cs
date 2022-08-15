using MyBlog.Application.Enum;
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
    public class GetAllPostQuery : IQuery<Result<PageList<PostForGetAllResponse>>>
    {
        public int PageNumer { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public string SearchString { get; set; } = "";
        public QueryPostStatus Status { get; set; } = QueryPostStatus.Published;
    }
}