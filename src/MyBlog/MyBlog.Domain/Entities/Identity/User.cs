
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Domain.Entities.Identity
{
    public class User:IdentityUser
    {
        public string UrlProfile { get; set; }
        public string Name { get; set; }
        public string SubPassword { get; set; }
    }
}
