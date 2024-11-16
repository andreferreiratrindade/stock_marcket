using Framework.Core.DomainObjects;
using Stock_Market.Api.Domain.Models.Entities.Ids;

namespace Stock_Market.Api.Domain.Models.Entities
{
    public class PriceStock : AggregateRoot, IAggregateRoot
    {
        public PriceStockId PriceStockId { get; }
        public StockId StockId { get; }
        public decimal Regular { get; }
        public decimal RegularDayHigh { get; }
        public decimal RegularDayLow { get;}
        public int RegularVolume { get; }
        public DateTime ReferenceTime { get; }

        public static PriceStock Create(StockId stockId,
            decimal regular,
            decimal regularDayHigh,
            decimal regularDayLow,
            int regularVolume,
            DateTime referenceTime,
            CorrelationId correlationId)
        {
            return new PriceStock(stockId, regular, regularDayHigh, regularDayLow, regularVolume, referenceTime, correlationId);
        }

        protected PriceStock()
        {

        }

        private PriceStock(StockId stockId ,decimal regular, 
            decimal regularDayHigh, 
            decimal regularDayLow, 
            int regularVolume, 
            DateTime referenceTime, CorrelationId correlationId){
            
            this.PriceStockId = new PriceStockId(Guid.NewGuid());
            this.StockId = stockId;
            this.Regular = regular;
            this.RegularDayHigh = regularDayHigh;
            this.RegularDayLow = regularDayLow;
            this.RegularVolume = regularVolume;
            this.ReferenceTime = referenceTime;
        }

        protected override void When(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
