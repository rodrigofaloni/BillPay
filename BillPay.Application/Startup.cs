using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BillPay.Application.Middlewares;
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
using Microsoft.OpenApi.Models;

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
            this.RegistraDadosSwagger(services);
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bill and Pay - V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureExceptionHandler();

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

        /// <summary>
        /// Responsável em registrar os dados swagger.
        /// </summary>
        /// <param name="services">Os services.</param>
        private void RegistraDadosSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "INTEGRATION API FOR ACCOUNTS PAYABLE",
                    Version = "v1",
                    Description = "REST API CREATED WITH ASP.NET CORE 3.1"
                });

                ////// Colocar JWT no Swagger
                ////options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                ////{
                ////    Description = "Cabeçalho de autorização JWT usando o esquema Bearer. \r\n\r\n Digite 'Bearer' [espaço] e, em seguida, seu token na entrada de texto abaixo.\r\n\r\nExemplo: \"Bearer 12345abcdef\"",
                ////    Name = "Authorization",
                ////    In = ParameterLocation.Header,
                ////    Type = SecuritySchemeType.ApiKey,
                ////    Scheme = "Bearer"
                ////});

                ////options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                ////{
                ////    {
                ////        new OpenApiSecurityScheme
                ////        {
                ////            Reference = new OpenApiReference
                ////            {
                ////                Type = ReferenceType.SecurityScheme,
                ////                Id = "Bearer"
                ////            },
                ////            Scheme = "oauth2",
                ////            Name = "Bearer",
                ////            In = ParameterLocation.Header,
                ////        },
                ////        new List<string>()
                ////    }
                ////});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(@".\App_Data", xmlFile);
                options.IncludeXmlComments(xmlPath, true);
            });
        }
    }
}
