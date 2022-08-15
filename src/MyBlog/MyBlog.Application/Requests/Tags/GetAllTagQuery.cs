using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Reponses.Tags;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Requests.Tags
{
    public class GetAllTagQuery:IQuery<Result<List<TagForResponse>>>
    {
       
    }
}
