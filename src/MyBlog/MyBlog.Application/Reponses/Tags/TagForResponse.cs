using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Reponses.Tags
{
    public class TagForResponse
    {
        public string Name { get; set; }
        public string TagLink { get; set; }
        public TagForResponse(string name,string tagLink)
        {
            Name = name;
            TagLink = tagLink;
        }  
    }
}
