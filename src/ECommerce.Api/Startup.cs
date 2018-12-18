﻿using AutoMapper;
using ECommerce.Api.Configuration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .AddInMemoryDatabase(Configuration)
                .AddAutoMapper()
                .AddGateways();

            services
                .AddMvc()
                .AddFluentValidation(v => v.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }

    public class StartupDevelopment : Startup
    {
        public StartupDevelopment(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            base.Configure(app, env);
        }
    }

    public class StartupProduction : Startup
    {
        public StartupProduction(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
