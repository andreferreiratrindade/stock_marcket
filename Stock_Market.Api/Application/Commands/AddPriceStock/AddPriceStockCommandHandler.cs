using Framework.Core.Mediator;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using Stock_Market.Api.Domain.Models.Entities;
using Stock_Market.Api.Domain.Models.Repositories;

namespace Stock_Market.Api.Application.Commands.AddPriceStock
{
    public class AddPriceStockCommandHandler(
        IStockRepository stockRepository,
        IPriceStockRepository priceStockRepository,
        IBrApiRepository brApiRepository,
        IDomainNotification domainNotification,
        IMediatorHandler mediatorHandler)
        : CommandHandler<AddPriceStockCommand, AddPriceStockCommandOutput, AddPriceStockCommandValidation>(domainNotification,
            mediatorHandler)

    {


        public override async Task<AddPriceStockCommandOutput> ExecutCommand(AddPriceStockCommand request, CancellationToken cancellationToken)
        {
            _domainNotification.AddNotification(await CheckValidations(request));
            if (_domainNotification.HasNotifications) return request.GetCommandOutput();
           var stock= await stockRepository.GetBySymbol(request.StockSymbol);
           if (stock == null)
           {
               _domainNotification.AddNotification("Error",$"Stock symbol {request.StockSymbol} not found");
               return request.GetCommandOutput();
           }

           var responseApiStock = await brApiRepository.GetStoke(request.StockSymbol);

           var priceStockEntity = PriceStock.Create(stock.StockId,
               responseApiStock.Regular,
               responseApiStock.RegularDayHigh,
               responseApiStock.RegularDayLow,
               responseApiStock.RegularVolume,
               responseApiStock.ReferenceTime,
               request.CorrelationId);

            priceStockRepository.Add(priceStockEntity);
            
            await PersistData(priceStockRepository.UnitOfWork);

            return new AddPriceStockCommandOutput
            {
             
            };

        }

        private Task<List<NotificationMessage>> CheckValidations(AddPriceStockCommand request)
        {
            var lstNotifications = new List<NotificationMessage>();
            return Task.FromResult(lstNotifications);
        }
    }
}
