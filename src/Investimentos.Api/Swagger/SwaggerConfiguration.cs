using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Investimentos.Api.Swagger
{
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(this IServiceCollection services)
        {
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

        public static void UseApplicationSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Investimentos API");
            });
        }
    }
}