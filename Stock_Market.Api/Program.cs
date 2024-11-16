using Stock_Market.Api.Api.Configuration;
using StockService.Api.Configuration;

namespace Stock_Market.Api;

public static class Program
{
    private static WebApplicationBuilder? _builder;
    private static WebApplication? _app;

    private static void Main(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);


        ConfigureServices();

        _app = _builder.Build();

        ConfigureRequestsPipeline();

        _app.Run();
    }

    private static void ConfigureServices()
    {
        _builder.AddApiConfiguration();
        _builder.AddSwaggerConfiguration();
    }

    private static void ConfigureRequestsPipeline()
    {
        _app.UseApiConfiguration();

        //  _app.UseProblemDetails();
        // _app.UseSerilogRequestLogging();
        _app.UseSwaggerConfiguration();

        _app.MigrationInitialization();
    }
}