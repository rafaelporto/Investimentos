using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Investimentos.Application.Configuration;
using System.Reflection;
using System.IO;
using System;
using System.Linq;

namespace Investimentos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AzureADB2CDefaults.BearerAuthenticationScheme)
                .AddAzureADB2CBearer(options => Configuration.Bind("AzureAdB2C", options));

            services.AddApplication(Configuration);

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", 
                        new OpenApiInfo 
                            {
                                Title = "Investimentos API",
                                Version = "v1",
                                Contact = new OpenApiContact
                                    {
                                        Name = "Rafael Monteiro Porto",
                                        Email = "rafael.monteiro.porto@outlook.com"
                                    },
                                Description = "Esta é a API de Investimentos do cliente."
                            });
                
                var projectsPrefixsName = "Investimentos";
                var pathToXmlDocumentsToLoad = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(x => x.FullName.StartsWith(projectsPrefixsName))
                        .Select(s => Path.Combine(AppContext.BaseDirectory, $"{s.GetName().Name}.xml"))
                        .ToList();

                pathToXmlDocumentsToLoad.ForEach(x => config.IncludeXmlComments(x));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Investimentos API");
            });

            //TODO Configure https dev-cert localhost
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
