using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Crm.Infrastructure.Database;
using Crm.Infrastructure.Processing;
using Crm.Api.Configuration;
using Crm.Application.Configuration;
using Microsoft.AspNetCore.Http;
using Crm.Infrastructure;
using System;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Crm.Infrastructure.Caching;
using Crm.Api.Configuration.Middlewares;
using Newtonsoft.Json;

namespace Crm.Api
{
    public class Startup
    {
        public IConfigurationRoot _configuration { get; private set; }
        private const string _connectionString = "CompaniesConnectionStrings";
        private const string allowedOrigins = "allowedOrigins";

        // in core 3 use to register .env variables
        public Startup(IWebHostEnvironment env)
        {
           _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {

            // Register your own things directly with Autofac, like:
            var connectionString = _configuration.GetConnectionString(_connectionString);
            builder.RegisterModule(new DataAccessModule(connectionString));
            builder.RegisterModule(new MediatorModule());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddMemoryCache();

            services.AddSwagger();

            services.AddOptions();

            services.AddControllers();

            services.AddMvc()
             .AddNewtonsoftJson(options =>
             {
                 options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             });

            services.AddCors(options =>
            {
                options.AddPolicy(allowedOrigins, builder =>
                {
                    builder.WithOrigins(_configuration.GetSection("AllowedOrigins").Get<string[]>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition");
                });
            });


            services.AddHttpContextAccessor();
            var serviceProvider = services.BuildServiceProvider();

            IExecutionContextAccessor executionContextAccessor = new ExecutionContextAccessor(serviceProvider.GetService<IHttpContextAccessor>());

            var children = _configuration.GetSection("Caching").GetChildren();
            var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
            var memoryCache = serviceProvider.GetService<IMemoryCache>();

            return ApplicationStartup.Initialize(services,
                _configuration[_connectionString],
               new MemoryCacheStore(memoryCache, cachingConfiguration),
               executionContextAccessor);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOptions();

            app.UseCors(allowedOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerDocumentation();
        }
    }
   
}
