using MyBlog.Application.Reponses.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Reponses.Posts
{
    public class PostForGetAllResponse
    {  
        public Guid Id { get; set; }
        public string Header { get; set; }
        public string HeaderLink { get; set; }
        public string ShortContent { get; set; }
        public string ImagePost { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset?Published { get; set; }
        public DateTimeOffset Updated { get; set; }
        public bool IsPublished { get; set; }
        public IEnumerable<TagForResponse> Tags { get; set; }   
    }
}
