using Microsoft.AspNetCore.Identity;
using MyBlog.Application.Models;
using MyBlog.Domain.Entities.Identity;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Application.Interfaces.Services.Admin
{
    public interface IAuthenticateService
    {
        Task<Result<IdentityBlogResult>> Authenticate(string userName, string passWord);
        Task<ClaimsIdentity> CreateIdentityFromUser(User user);
        IdentityBlogResult User { get; }

    }
}
