using Microsoft.AspNetCore.Identity;
using MyBlog.Application.Interfaces.Services.Admin;
using MyBlog.Application.Models;
using MyBlog.Domain.Entities.Identity;
using MyBlog.Share.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Infrastructure.Services
{
    public class AuthenticateService:IAuthenticateService
    {
        private UserManager<User> _userManager;
        public AuthenticateService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Result<IdentityBlogResult>> Authenticate(string userName, string passWord)
        {
           
          var userInDb=await  _userManager.FindByNameAsync(userName);
            if (userInDb != null)
            {
               bool checkPassword= await _userManager.CheckPasswordAsync(userInDb, passWord);

                if (checkPassword == true)
                {
                    var roles = await _userManager.GetRolesAsync(userInDb);
                    User = new IdentityBlogResult(userInDb, roles);
                    return Result<IdentityBlogResult>.Success(new IdentityBlogResult(userInDb,roles));

                }

            }
            return Result<IdentityBlogResult>.Fail("Login failed");
        }
        public IdentityBlogResult User { get; private set; }
        public async Task<ClaimsIdentity> CreateIdentityFromUser(User user)
        {
            var result = new ClaimsIdentity(new Claim[]
            {   new(ClaimTypes.NameIdentifier,user.Id),
                new (ClaimTypes.Name, user.UserName),
                new (ClaimTypes.Hash, user.PasswordHash),

            }, "identityUser");

           var roles= await _userManager.GetRolesAsync(user);

            foreach (string role in roles)
            {
                result.AddClaim(new(ClaimTypes.Role, role));
            }

            return result;
        }

    
    }

}
