using Framework.Core.Data;
using Stock_Market.Api.Domain.Models.Entities;
using Stock_Market.Api.Domain.Models.Entities.Ids;

namespace Stock_Market.Api.Domain.Models.Repositories
{
    public interface IStockRepository : IRepository<Stock>
    {

        Task<Stock> GetById(StockId StockId);

        Task<Stock> GetBySymbol(string symbol);
    }
}
