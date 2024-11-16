

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.OpenTelemetry;

namespace Framework.Core.OpenTelemetry
{
    public static class OpenTelemetryConfig
    {
        public static void RegisterOpenTelemetry(this WebApplicationBuilder builder)
        {
            var serviceName = builder.Configuration.GetSection("NameApp").Value;

            builder.Host.UseSerilog((context, loggerConfiguration)=>{
                loggerConfiguration.WriteTo.OpenTelemetry(opts =>
                                            {
                                                opts.Endpoint = builder.Configuration.GetSection("OpenTelemetryURL").Value;
                                                opts.Protocol = OtlpProtocol.Grpc;
                                                opts.IncludedData = IncludedData.SpecRequiredResourceAttributes;
                                                opts.ResourceAttributes = new Dictionary<string, object>
                                                {
                                                    ["app"] = "web",
                                                    ["runtime"] = "dotnet",
                                                    ["service.name"] = serviceName
                                                };
                                            });
            });
            builder.Services
                .AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(serviceName))
                .WithLogging(builderOtel =>
                {
                    builderOtel.AddConsoleExporter();

                    builderOtel.AddOtlpExporter(opts =>
                   {

                       opts.Endpoint = new Uri(builder.Configuration.GetSection("OpenTelemetryURL").Value);
                   });
                })
                .WithTracing(builderOtel =>
                {
                    builderOtel
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation()
                        .AddRedisInstrumentation()
                        .AddNpgsql()
                    .AddConsoleExporter()

                    .AddOtlpExporter(opts =>
                    {
                        opts.Endpoint = new Uri(builder.Configuration.GetSection("OpenTelemetryURL").Value);
                    });
                });
        }
    }
}
