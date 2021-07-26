using AutoMapper;
using FluentValidation;
using InHouse.Application.Common.Behaviours;
using InHouse.Application.Restaurant.Listing.V1.Usecases.Interfaces;
using InHouse.Application.Restaurant.Listing.V1.Usecases.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace InHouse.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, bool azureFuncOnly = false)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            // register Usecases

            services.AddScoped<ITransformWorkingHoursService, TransformWorkingHoursService>();

            return services;
        }
    }
}
