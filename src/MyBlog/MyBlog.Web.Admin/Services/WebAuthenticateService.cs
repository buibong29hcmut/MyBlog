using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Interfaces.Services.Admin;
using MyBlog.Domain.Entities.Identity;
using MyBlog.Infrastructure.Services;
using MyBlog.Share.Wrappers;
using MyBlog.Web.Admin.Models;
using Newtonsoft.Json;
using System;
using System.Security.Claims;

namespace MyBlog.Web.Admin.Services
{
    public class WebAuthenticateService : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;
        private readonly IUnitOfWork _unit;
        private readonly IAuthenticateService _authenticateService;
        private readonly IDistributedCache _distributedCache;
        private readonly BlazorAppContext _appState;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WebAuthenticateService(ProtectedLocalStorage protectedLocalStorage,
            IUnitOfWork unit, 
            IAuthenticateService authenticateService,
            IDistributedCache distributedCache,
            BlazorAppContext appState,
            IHttpContextAccessor httpContextAccessor)
        {
            _protectedLocalStorage = protectedLocalStorage;
            _unit = unit;
            _authenticateService = authenticateService;
            _distributedCache = distributedCache;
            _appState = appState;
            _httpContextAccessor = httpContextAccessor; 
        }
        public UserDto User { get; private set; }
     
    
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var principal = await GetClaimPrincipal();
            return new AuthenticationState(principal);
        }
  
        private async Task<ClaimsPrincipal> GetClaimPrincipal()
        {
            var principal = new ClaimsPrincipal();
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {
              byte[] byteStoredIdentity = await _distributedCache.GetAsync(ip);
                if (byteStoredIdentity!=null)
                {
                    var jsonToDeserialize = System.Text.Encoding.UTF8.GetString(byteStoredIdentity);
                    var userInBrowerStorage = JsonConvert.DeserializeObject<UserDto>(jsonToDeserialize);
                   var result= await  _authenticateService.Authenticate(userInBrowerStorage.UserName, userInBrowerStorage.Password);
                    if (result.Succeeded)
                    {
                        var identity = await CreateIdentityFromUser(userInBrowerStorage);
                        return new ClaimsPrincipal(identity);
                    }
           
                }
           
            } 
            catch(Exception ex)
            {
                
        
            }
            return principal;

        }
   
        public async Task<bool> LoginAsync(LoginFormModel loginFormModel)
        {
            var identityResult =await _authenticateService.Authenticate(loginFormModel.UserName, loginFormModel.Password);
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            var principal = new ClaimsPrincipal();
            if (!identityResult.Succeeded)
            {
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));

                return false;
            }
                var result = identityResult.Data;
        
                User = new UserDto()
                { UserId = result.User.Id,
                    UserName = result.User.UserName,
                    Email = result.User.Email,
                    Password=loginFormModel.Password,
                    ImageProfile = result.User.UrlProfile,
                    Roles = result.Roles.ToList()
                };
                var json = JsonConvert.SerializeObject(User);
                await _distributedCache.SetStringAsync(ip, json);
                var identity = await  CreateIdentityFromUser(User);
                principal = new ClaimsPrincipal(identity);
          
            

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
            return true;
           
        }

        public async Task LogoutAsync()
        {
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            await _distributedCache.RemoveAsync(ip);
            var principal = new ClaimsPrincipal();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }
        public async Task<ClaimsIdentity> CreateIdentityFromUser(UserDto user)
        {
            var result = new ClaimsIdentity(new Claim[]
            {   new(ClaimTypes.NameIdentifier,user.UserId),
                new (ClaimTypes.Name, user.UserName),

            }, "identityUser");

            foreach (string role in user.Roles)
            {
                result.AddClaim(new(ClaimTypes.Role, role));
            }

            return await Task.FromResult(result);
        }


    }
}
