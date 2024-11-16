using System.Data.Common;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Stock_Market.Api.Domain.Models.Entities;
using Stock_Market.Api.Domain.Models.Entities.Ids;
using Stock_Market.Api.Domain.Models.Repositories;

namespace Stock_Market.Api.Infra.Data.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly StockContext _context;
        private readonly IMemoryCache _memoryCache;

        public StockRepository(StockContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }
        public IUnitOfWork UnitOfWork => _context;


        public void Dispose()
        {
            _context.Dispose();
        }

        public  Task<Stock?> GetById(StockId StockId)
        {
            return  _memoryCache.GetOrCreate(StockId,  entry =>
         {
             return  _context.Stocks.FirstOrDefaultAsync(x => x.StockId == StockId);
         });
        }

        public  Task<Stock> GetBySymbol(string symbol)
        {
            return  _memoryCache.GetOrCreate(symbol,  entry =>
        {
            return  _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        });
        }

        public DbConnection GetConnection() => _context.Database.GetDbConnection();


    }

}
