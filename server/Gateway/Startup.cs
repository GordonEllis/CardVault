﻿using Gateway.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueueHandler;

namespace Gateway
{
    public class Startup
    {
        #region -  Constructor  -

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.GetSection(nameof(QueueingConfiguration)).Bind(Queueing = new QueueingConfiguration());
        }

        #endregion

        #region -  Properties  - 

        public IConfiguration Configuration { get; }
        public QueueingConfiguration Queueing { get; }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureMvc(services);
            this.ConfigureQueueing(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
