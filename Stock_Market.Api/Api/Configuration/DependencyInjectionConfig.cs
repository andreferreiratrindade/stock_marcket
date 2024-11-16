using Framework.Core.Data;
using Framework.Core.Mediator;
using Framework.Core.MongoDb;
using Framework.Core.OpenTelemetry;
using Framework.WebApi.Core.Configuration;
using MassTransit;
using MediatR;
using Stock_Market.Api.Application.Commands.AddPriceStock;
using Stock_Market.Api.Application.Queries;
using Stock_Market.Api.Domain.Models.Repositories;
using Stock_Market.Api.Infra;
using Stock_Market.Api.Infra.Data.Repository;

namespace Stock_Market.Api.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.RegisterMediatorBehavior(typeof(Program).Assembly);

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            ApiConfigurationWebApiCore.RegisterServices(builder.Services);

            builder.Services.RegisterRepositories();
            builder.Services.RegisterCommands();
            builder.Services.RegisterRules();
            builder.Services.RegisterQueries();
            builder.Services.RegisterIntegrationService();
            builder.Services.RegisterEvents();
            builder.RegisterEventStored();
            builder.RegisterOpenTelemetry();
            builder.Services.AddMemoryCache();

        }

        private static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {

           
        }

        private static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IPriceStockRepository, PriceStockRepository>();
            services.AddScoped<IBrApiRepository, BrApiRepository>();
        }

        private static void RegisterCommands(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AddPriceStockCommand, AddPriceStockCommandOutput>, AddPriceStockCommandHandler>();
            
        }

        private static void RegisterRules(this IServiceCollection services)
        {


        }


        private static void RegisterQueries(this IServiceCollection services)
        {

            services.AddScoped<IPriceStockQuery, PriceStockQuery>();

        }

        private static void RegisterIntegrationService(this IServiceCollection services)
        {

        }

        private static void RegisterEvents(this IServiceCollection services)
        {
            // services.AddScoped<INotificationHandler<TransactionPurchaseRequestedEvent>, TransactionPurchaseRequestedEventHandler>();
            // services.AddScoped<INotificationHandler<TransactionSoldRequestedEvent>, TransactionSoldRequestedEventHandler>();


        }

        private static void RegisterEventStored(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));

            builder.Services.AddScoped<IEventStored, EventStored>();
            builder.Services.AddScoped<IEventStoredRepository, EventStoredRepository>();
        }
    }
}
