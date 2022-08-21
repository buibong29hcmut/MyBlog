using MyBlog.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Models
{
    public class IdentityBlogResult
    {
        public User User { get; private set; }
        public IdentityBlogResult(User user, IList<string> roles)
        {
            User = user;
            Roles = roles;  
        }
        public IList<string> Roles { get; private set; }
    }
}
