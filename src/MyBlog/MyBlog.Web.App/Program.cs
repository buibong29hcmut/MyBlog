using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using MyBlog.Application;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Interfaces.Services.Admin;
using MyBlog.Domain.Entities.Identity;
using MyBlog.Infrastructure;
using MyBlog.Infrastructure.Cọntexts;
using MyBlog.Web.App;
using MyBlog.Web.App.Components;
using MyBlog.Web.App.Extensions;
using MyBlog.Web.App.SEOs;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseWebRoot("wwwroot").UseStaticWebAssets();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton(new AppState());

builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddConsole()
        .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
    loggingBuilder.AddDebug();
});
builder.Services.AddSignalR()
               .AddAzureSignalR(builder.Configuration.GetValue<string>("AzureSignalRConnectionstring"))
               ;
builder.Services.AddInfrastructure(builder.Configuration,typeof(Program),typeof(MyBlogDbContext),typeof(IRepository<>),typeof(IUnitOfWork));
builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<MyBlogDbContext>()
            .AddDefaultTokenProviders();
builder.Services.AddScoped<MetaDataState>();
//builder.Services.AddSingleton<MetadataProvider>();
//builder.Services.AddScoped<MetadataTransferService>();

//builder.Services.AddSingleton<TagComponent>();
var app = builder.Build();



if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
