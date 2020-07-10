using System;
using System.Linq;
using System.Net.Mime;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace Investimentos.Api.HealthCheck
{
    public static class HealthCheckConfiguration
    {
        public static void AddApplicationHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                        .AddPrivateMemoryHealthCheck(1024 * 1024 * 1024, "Privatememory")
                        .AddWorkingSetHealthCheck(1024 * 1024 * 1024);
            services.AddHealthChecksUI().AddInMemoryStorage();
        }

        public static void UseApplicationHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/status",
               new HealthCheckOptions()
               {
                   ResponseWriter = async (context, report) =>
                   {
                       var result = JsonConvert.SerializeObject(
                           new
                           {
                               statusApplication = report.Status.ToString(),
                               healthChecks = report.Entries.Select(e => new
                               {
                                   check = e.Key,
                                   ErrorMessage = e.Value.Exception?.Message,
                                   status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                               })
                           });
                       context.Response.ContentType = MediaTypeNames.Application.Json;
                       await context.Response.WriteAsync(result);
                   }
                });
            
            app.UseHealthChecks("/healthchecks-data-ui", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI();
        }
    }
}