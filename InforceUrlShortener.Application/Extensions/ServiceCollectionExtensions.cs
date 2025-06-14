﻿using FluentValidation;
using FluentValidation.AspNetCore;
using InforceUrlShortener.Application.User;
using Microsoft.Extensions.DependencyInjection;

namespace InforceUrlShortener.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(ServiceCollectionExtensions).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly)
                .AddFluentValidationAutoValidation();

            services.AddScoped<IUserContext, UserContext>();
            services.AddHttpContextAccessor();
        }
    }
}
