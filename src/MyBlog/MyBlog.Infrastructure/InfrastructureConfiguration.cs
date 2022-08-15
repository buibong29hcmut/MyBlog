using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Interfaces.Services.Admin;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Domain.Entities.Identity;
using MyBlog.Infrastructure.Cọntexts;
using MyBlog.Infrastructure.Repositories;
using MyBlog.Infrastructure.Services;
using System;
using System.Linq;
using System.Reflection;

namespace MyBlog.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration, params Type[] types)
        {

            var assemblies = types.Select(type => type.GetTypeInfo().Assembly);

            foreach (var assembly in assemblies)
            {
                services.AddMediatR(assembly);
            }
            var connectionString = configuration.GetConnectionString("BlogDb");
            services.AddDbContext<MyBlogDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging(true);
            });
            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString =    connectionString;
                options.SchemaName = "dbo";
                options.TableName = "BlogCaches";
            });
            services.AddDbContext<MyBlogDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(""));
                options.EnableSensitiveDataLogging();
            },ServiceLifetime.Transient);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICommandBus, CommandBus>();
            services.AddTransient<IQueryBus, QueryBus>();
          
            return services;
        }
        public static void AddUploadFileService(this IServiceCollection services)
        {
            services.AddScoped<ISaveFileService, SaveFileService>();
        }
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole),
           builder.Services);
            builder.AddEntityFrameworkStores<MyBlogDbContext>().AddDefaultTokenProviders();
            return services;

        }
    }
}
