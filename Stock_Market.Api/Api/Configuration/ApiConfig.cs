using Microsoft.EntityFrameworkCore;
using Stock_Market.Api.Domain.Models.Entities;
using Stock_Market.Api.Infra;
using StockService.Api.Configuration;

namespace Stock_Market.Api.Api.Configuration
{
    public static class ApiConfig
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<StockContext>(options =>
                options.UseSqlServer(builder.Configuration["ConnectionStringSql"]));
            builder.Services.AddDbContext<StockContext>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpClient();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("all",
                   builder =>
                       builder
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            builder.RegisterServices();
            return builder;

        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            app.UseRouting();

            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            //app.MapGraphQL();
              app.MapControllers();
            return app;
        }
    }
}
