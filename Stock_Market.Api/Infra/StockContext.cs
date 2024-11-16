using Framework.Core.Data;
using Framework.Core.Mediator;
using Microsoft.EntityFrameworkCore;
using Stock_Market.Api.Domain.Models.Entities;

namespace Stock_Market.Api.Infra
{
    public class StockContext(
        DbContextOptions<StockContext> options,
        IEventStored eventStored,
        IMediatorHandler mediatorHandler)
        : DbContextCustom<StockContext>(options, eventStored, mediatorHandler)
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<PriceStock> PricesStock { get; set; }

    

    public  void LoadStockList()
    {
        var stocks = new List<Stock>
        {
            Stock.Create("ITAU", "ITSA4"),
            Stock.Create("Ambev", "ABEV3"),
            Stock.Create("Usiminas", "USIM4")
        };

        var newStocks = stocks.Where(x=> Stocks.Select(y => y.Symbol).ToList().All(y => y != x.Symbol)).ToList();

        Stocks.AddRange(newStocks);
        this.SaveChanges();
    }
}

}