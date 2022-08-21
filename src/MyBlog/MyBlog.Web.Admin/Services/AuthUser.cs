using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Domain.Entities.Identity;
using MyBlog.Web.Admin.Models;
using System.Data;

namespace MyBlog.Web.Admin.Services
{
    public class AuthUser
    {
        private readonly WebAuthenticateService _webAuthenticateService;
        private readonly UserManager<User> _userManager;
        public AuthUser(WebAuthenticateService webAuthenticateService, UserManager<User> _userManager)
        {
            _webAuthenticateService = webAuthenticateService;
            var username = _webAuthenticateService.GetAuthenticationStateAsync().Result;
            this._userManager = _userManager;
            FetchMyUser(username.User.Identity.Name);
            
        }

        public UserDto MyUser { get; private set; }

        public void FetchMyUser(string machineName  )
        {
            var user =  _userManager.Users
                .Where(p => p.UserName.Equals(machineName)).Select(p => new User()
                {
                    Id = p.Id,
                    UserName = p.UserName,
                    UrlProfile = p.UrlProfile,
                    Email = p.Email,

                }).FirstOrDefault();
            var roles = _userManager.GetRolesAsync(user).Result.ToList();
            MyUser = new UserDto()
            {
                UserId = user.Id,
                UserName = user.UserName,
                ImageProfile = user.UrlProfile,
                Email = user.Email,
                Roles = roles
            };

        }
    }
}
