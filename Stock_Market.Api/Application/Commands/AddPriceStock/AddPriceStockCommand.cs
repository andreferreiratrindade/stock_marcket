using System.ComponentModel.DataAnnotations;
using Framework.Core.DomainObjects;
using Framework.Core.Messages;
using Stock_Market.Api.Domain.DomainEvents;
using Stock_Market.Api.Domain.Models.Entities.Ids;

namespace Stock_Market.Api.Application.Commands.AddPriceStock
{
    public class AddPriceStockCommand : Command<AddPriceStockCommandOutput>
    {

        public string StockSymbol { get; set; }



        public AddPriceStockCommand( string stockSymbol):base(CorrelationId.Create())
        {
            StockSymbol = stockSymbol;
            

            this.AddRollBackEvent(new PriceStockAddedCompensationEvent(this.StockSymbol,this.CorrelationId));
        }
    }

    public class AddPriceStockCommandOutput : OutputCommand
    {


    }
}
