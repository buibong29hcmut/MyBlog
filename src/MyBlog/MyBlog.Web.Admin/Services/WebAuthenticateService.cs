using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Web.Admin.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MyBlog.Web.Admin.Services
{
    public class WebAuthenticateService : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly IUnitOfWork _unit;

        public WebAuthenticateService(ProtectedLocalStorage protectedLocalStorage, IUnitOfWork unit)
        {
            _protectedLocalStorage = protectedLocalStorage;
            _unit = unit;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var principal = new ClaimsPrincipal();

            try
            {
                var storedPrincipal = await _protectedLocalStorage.GetAsync<string>("identity");

                if (storedPrincipal.Success)
                {
                    var user = JsonConvert.DeserializeObject<User>(storedPrincipal.Value);
                    var (userInDb, isLookUpSuccess) = LookUpUser(user.UserName, user.Password);

                    if (isLookUpSuccess)
                    {
                        var identity = CreateIdentityFromUser(userInDb);
                        principal = new(identity);
                    }
                }
            }
            catch
            {

            }

            return new AuthenticationState(principal);
        }

        public async Task LoginAsync(LoginFormModel loginFormModel)
        {
            var (userInDatabase, isSuccess) = LookUpUser(loginFormModel.UserName, loginFormModel.Password);
            var principal = new ClaimsPrincipal();

            if (isSuccess)
            {
                var identity = CreateIdentityFromUser(userInDatabase);
                principal = new ClaimsPrincipal(identity);
                await _protectedLocalStorage.SetAsync("identity", JsonConvert.SerializeObject(userInDatabase));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public async Task LogoutAsync()
        {
            await _protectedLocalStorage.DeleteAsync("identity");
            var principal = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        private ClaimsIdentity CreateIdentityFromUser(User user)
        {
            var result = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name, user.UserName),
                new (ClaimTypes.Hash, user.Password),
            
            }, "BlazorSchool");


            foreach (string role in user.Roles)
            {
                result.AddClaim(new(ClaimTypes.Role, role));
            }

            return result;
        }

        private (User, bool) LookUpUser(string username, string password)
        { User result = null;
            if( username == "buibong2912" && password == "29122002Az@")
            {
               result= new User()
                {
                    UserName = username,
                    Password = password
                };
            };

            return (result, result is not null);
        }
    }
}
