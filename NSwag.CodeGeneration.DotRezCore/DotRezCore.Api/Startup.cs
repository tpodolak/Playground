using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSwaggerGen(
                options =>
                {
                    options.CustomSchemaIds(type => type.FullName);
                    options.DescribeAllEnumsAsStrings();
                    options.OperationFilter<UseOpenApi3OperationId>();
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/Sample API/swagger.json", "swagger"));

        }
    }
}