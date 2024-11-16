using Framework.Core.DomainObjects;
using Stock_Market.Api.Domain.Models.Entities.Ids;

namespace Stock_Market.Api.Domain.DomainEvents
{
    public class PriceStockAddedEvent(
        PriceStockId priceStockId,
        StockId stockId, 
        decimal regular,
        decimal regularDayHigh,
        decimal regularDayLow,
        int regularVolume,
        DateTime referenceTime,
        CorrelationId correlationId)
        : DomainEvent(correlationId)
    {
        public StockId StockId { get; set; } = stockId;
        public decimal Regular { get; set; } = regular;
        public decimal RegularDayHigh { get; } = regularDayHigh;
        public decimal RegularDayLow { get;} = regularDayLow;
        public int RegularVolume { get; } = regularVolume;
        public DateTime ReferenceTime { get; } = referenceTime;
    }
    
    public class PriceStockAddedCompensationEvent : RollBackEvent
    {
        public PriceStockAddedCompensationEvent(string stockSymbol,  CorrelationId correlationId):base(correlationId)
        {

        }
    }
}
