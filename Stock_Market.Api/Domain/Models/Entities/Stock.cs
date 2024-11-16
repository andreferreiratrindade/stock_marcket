using Framework.Core.DomainObjects;
using Stock_Market.Api.Domain.Models.Entities.Ids;

namespace Stock_Market.Api.Domain.Models.Entities
{
    public class Stock : AggregateRoot, IAggregateRoot
    {

        public StockId StockId{get;}
        //public Guid Id {get {return StockId.Value ; }}
        public string Name {get;}
        public string Symbol {get;}

        public static Stock Create(string name, string symbol){
            return new Stock(name, symbol);
        }

        protected Stock()
        {

        }

        private Stock(string name, string symbol){
            this.StockId = new StockId(Guid.NewGuid());
            this.Name = name;
            this.Symbol = symbol;
        }

        protected override void When(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
