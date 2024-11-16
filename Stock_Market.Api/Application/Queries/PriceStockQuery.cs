using MassTransit.Initializers;
using Stock_Market.Api.Domain.Models.Repositories;
using Stock_Market.Api.Infra.Data.Dtos;
using Stock_Market.Api.Infra.Data.Repository;
using System.Linq;
namespace Stock_Market.Api.Application.Queries;

public class PriceStockQuery(IPriceStockRepository priceStockRepository, IStockRepository stockRepository ) : IPriceStockQuery
{
   
    public async Task<List<PriceStockDto>> GetPriceStock(string stockSymbol)
    {
        var stock = await stockRepository.GetBySymbol(stockSymbol);
        var prices = await priceStockRepository.GetByStockId(stock.StockId);

        return prices.Select(x => new PriceStockDto(stock.Symbol,
            x.Regular,
            x.RegularDayHigh,
            x.RegularDayLow,
            x.RegularVolume,
            x.ReferenceTime)
        ).ToList();
    }
}