using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotRezCore.Api.Extensions;
using DotRezCore.Api.OperationFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace DotRezCore.Api
{
    public class Startup : IStartup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfigurationBuilder _configurationBuilder;

        public Startup(IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory, IConfigurationBuilder configurationBuilder)
        {
            _hostingEnvironment = hostingEnvironment;
            _loggerFactory = loggerFactory;
            _configurationBuilder = configurationBuilder;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var configurationRoot = _configurationBuilder.AddJsonFile("foo.json", optional: true).Build();

            services.AddMvc();
            services.AddSwaggerGen(
                options =>
                {
                    options.CustomSchemaIds(type => type.FullName);
                    
                    if (configurationRoot.GetValue(Constants.CommandLineArguments.UseCustomSwaggerSchema, false))
                    {
                        options.UseCodeGeneratorFilters();
                    }

                    options.DescribeAllEnumsAsStrings();

                    options.SwaggerDoc("Sample API", new Info
                    {
                        Title = $"Sample API",
                        Version = "1",
                        Description = "A sample application with Swagger, Swashbuckle, and API versioning.",
                        Contact = new Contact {Name = "Bill Mei", Email = "bill.mei@somewhere.com"},
                        TermsOfService = "Shareware",
                        License = new License {Name = "MIT", Url = "https://opensource.org/licenses/MIT"}
                    });
                });

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            _loggerFactory.AddConsole();
            _loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/Sample API/swagger.json", "swagger"));
        }
    }
}