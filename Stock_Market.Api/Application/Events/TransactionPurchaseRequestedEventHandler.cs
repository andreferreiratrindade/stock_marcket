using MediatR;
using MassTransit;
using Framework.MessageBus;
using Framework.Shared.IntegrationEvent.Integration;
using Stock_Market.Api.Domain.Models.Repositories;

namespace StockService.Application.Events
{
    public class TransactionPurchaseRequestedEventHandler : INotificationHandler<TransactionPurchaseRequestedEvent>
    {
        private readonly IMessageBus _messageBus;
        private readonly IStockRepository _stockRepository;
        public TransactionPurchaseRequestedEventHandler(IMessageBus messageBus, IStockRepository stockRepository)
        {
         
            
        }

        public  Task Handle(TransactionPurchaseRequestedEvent message, CancellationToken cancellationToken)
        {
           return Task.CompletedTask;
           
        }
    }
}
