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
    public class GetAllPostByTagQuery:IQuery<Result<PageList<PostForGetAllByTagName>>>
    {  
        public string TagName { get; set; }
        public int PageNumer { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public QueryPostStatus Status { get; set; } = QueryPostStatus.Published;


    }
}
