using System.Data.Common;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using Stock_Market.Api.Domain.Models.Entities;
using Stock_Market.Api.Domain.Models.Entities.Ids;
using Stock_Market.Api.Domain.Models.Repositories;

namespace Stock_Market.Api.Infra.Data.Repository
{
    public class PriceStockRepository(StockContext context) : IPriceStockRepository
    {
        public IUnitOfWork UnitOfWork => context;

        public void Add(PriceStock priceStock)
        {
            context.PricesStock.Add(priceStock);
        }

        public async Task<List<PriceStock>> GetByStockId(StockId stockId)
        {
            return await  context.PricesStock.AsQueryable().Where(p => p.StockId == stockId).ToListAsync();
        }
        

        public void Dispose()
        {
            context.Dispose();
        }

        public DbConnection GetConnection() => context.Database.GetDbConnection();



   }

}
