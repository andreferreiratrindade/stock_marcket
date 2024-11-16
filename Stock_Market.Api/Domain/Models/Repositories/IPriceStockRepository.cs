using Framework.Core.Data;
using Stock_Market.Api.Domain.Models.Entities;
using Stock_Market.Api.Domain.Models.Entities.Ids;

namespace Stock_Market.Api.Domain.Models.Repositories
{
    public interface IPriceStockRepository : IRepository<PriceStock>
    {
        void Add(PriceStock stockResultTransaction);
        Task<List<PriceStock>>  GetByStockId(StockId stockId);
    }
}
