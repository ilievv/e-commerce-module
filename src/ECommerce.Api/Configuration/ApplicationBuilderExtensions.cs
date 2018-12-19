﻿using ECommerce.Api.Middlewares;
using ECommerce.Core.Gateways;
using ECommerce.Infrastructure.Data.Prototyping;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Api.Configuration
{
    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedFakeData(this IApplicationBuilder @this)
        {
            using (var scope = @this.ApplicationServices.CreateScope())
            {
                var providerManager = scope.ServiceProvider.GetService<IProviderManager>();
                var dataSeeder = new DataSeeder(providerManager);

                dataSeeder.Seed();
            }

            return @this;
        }

        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder @this)
        {
            @this.UseMiddleware<ExceptionMiddleware>();

            return @this;
        }

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder @this)
        {
            @this.UseSwagger();

            @this.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "E-commerce API");
                options.RoutePrefix = string.Empty;
            });

            return @this;
        }
    }
}
