using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillPay.Data;
using BillPay.Domain.Interface.Repository;
using BillPay.Domain.Interface.Service;
using BillPay.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BillPay.Application
{
    /// <summary>
    /// The startup class of application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BaseContext>(options => options.UseInMemoryDatabase(databaseName: "MockDB"));
            this.RegisterDependencies(services);
            services.AddControllers();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Registers the dependencies.
        /// </summary>
        /// <param name="services">The services.</param>
        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IBillRepository, BillRepository>();
        }
    }
}
