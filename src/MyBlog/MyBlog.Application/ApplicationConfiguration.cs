using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Reflection;
using MyBlog.Application.Commands.Abstracts;
using MyBlog.Application.Queries.Abstracts;
using MyBlog.Application.Interfaces.Repositories;
using MyBlog.Application.Behaviors;
using FluentValidation;

namespace MyBlog.Application
{
    public static  class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services /*params Type[] types*/)
        {

         
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
