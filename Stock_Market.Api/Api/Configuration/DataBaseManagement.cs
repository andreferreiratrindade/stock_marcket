using Microsoft.EntityFrameworkCore;
using Stock_Market.Api.Infra;

namespace Stock_Market.Api.Api.Configuration
{
     public static class DataBaseManagement
    {
        public static void MigrationInitialization (this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<StockContext>();

                db.Database.Migrate();
                db.LoadStockList();
            }
        }
    }
}
