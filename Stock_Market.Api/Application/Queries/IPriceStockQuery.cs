using Stock_Market.Api.Infra.Data.Dtos;

namespace Stock_Market.Api.Application.Queries;

public interface IPriceStockQuery
{
    Task<List<PriceStockDto>> GetPriceStock(string stockSymbol);
}