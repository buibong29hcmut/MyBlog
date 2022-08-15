using MyBlog.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Interfaces.Services.Admin
{
    public  interface ITokenService
    {
         Task<string> GenerateTokenAsync(User user);
    }
}
