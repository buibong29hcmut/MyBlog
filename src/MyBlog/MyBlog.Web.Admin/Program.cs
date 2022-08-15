using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Primitives;
using MudBlazor.Services;
using MyBlog.Application;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Interfaces.Services.Admin;
using MyBlog.Domain.Entities.Identity;
using MyBlog.Infrastructure;
using MyBlog.Infrastructure.Cọntexts;
using MyBlog.Web.Admin;
using MyBlog.Web.Admin.Interfaces;
using MyBlog.Web.Admin.Pages.Applications.Email;
using MyBlog.Web.Admin.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMudServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorPages();
builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddConsole()
        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
    loggingBuilder.AddDebug();
});

builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient<IUploadFileService>(p=>
{
    p.BaseAddress = new Uri("https://localhost:7256");
    p.DefaultRequestHeaders.Add("API_KEY", "29122002Az@");
 });
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IUploadFileService, UploadFileService>();
builder.Services.AddUploadFileService();
builder.Services.AddApplication();
builder.Services.AddScoped<WebAuthenticateService>();
builder.Services.AddInfrastructure(builder.Configuration, 
                                 typeof(Program), typeof(MyBlogDbContext),
                                 typeof(IRepository<>), typeof(IUnitOfWork))
                .ConfigureIdentity();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<WebAuthenticateService>());
;

var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
 
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
//app.UseMiddleware<ApiKeyMiddleWare>();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapPost("/api/uploadfile",async (ISaveFileService saver,IApiKeyService apiKeyService, IHttpContextAccessor http, HttpRequest request) =>
{
    bool haveApiKey = http.HttpContext.Request.Headers.TryGetValue("API_KEY", out StringValues apiKeyFromrequest);
    if (haveApiKey)
    {
      bool result=  await apiKeyService.ValidateApiKeyAsync(apiKeyFromrequest);
        if (result == true)
        {
            var file = request.Form.Files[0];
            var baseUrl = $"{http.HttpContext.Request.Scheme}://{http.HttpContext.Request.Host}{http.HttpContext.Request.PathBase}";
            string urlImage = await saver.UploadAsync(file);
            return Results.Ok(new { Url = urlImage });
        }
    }
  
   
    return Results.Unauthorized();
});
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
