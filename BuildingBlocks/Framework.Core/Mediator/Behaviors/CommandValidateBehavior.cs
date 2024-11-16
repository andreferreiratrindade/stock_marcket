using Framework.Core.LogHelpers;
using Framework.Core.Messages;
using Framework.Core.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Framework.Core.Mediator
{

    public class CommandValidateBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : OutputCommand
        where TRequest : Command<TResponse>
    {
        private readonly IDomainNotification _domainNotification;
        private readonly ILogger _logger;
        protected readonly IMediatorHandler _mediatorHandler;


        public CommandValidateBehavior(IMediatorHandler mediatorHandler,
                                        IDomainNotification domainNotification,
                                        ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
        {
            _domainNotification = domainNotification;
            _mediatorHandler = mediatorHandler;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _domainNotification.SetCorrelationId(request.CorrelationId);

            if (request.GetRollBackEvent() == null)
                throw new Exception("You need to add ValidationResult and rollBackEvent in your class command's constructor, doing something like that: this.AddValidCommand(new AddActivityCommandValidation().Validate(this)); this.AddCommandOutput(new AddActivityCommandOutput()); ");

            TResponse response = default;

            try
            {
                response = await next();
            }
            catch (Exception ex)
            {
                _domainNotification.AddNotification(ex);
                response = request.GetCommandOutput();
            }

            response = await CheckNotifications(request, response);


            response.CorrelationId = request.CorrelationId;
            return response;
        }

        private async Task<TResponse> CheckNotifications(TRequest request, TResponse response)
        {


            if (_domainNotification.HasNotifications)
            {
                var notificationsJson = JsonSerializer.Serialize(_domainNotification.Notifications);

                if (_domainNotification.HasNotificationWithException)
                {
                    _logger.CreateLog(new CriticErroLog(request.CorrelationId,
                                                    request.MessageType,
                                                    [LogConstants.ERROR_CRITIC],
                                                    notificationsJson));
                }
                else
                {
                       _logger.CreateLog(new GenericLog(request.CorrelationId,
                                                    request.MessageType,
                                                    [LogConstants.ERROR_INFORMATION],
                                                    notificationsJson));
                }

                var @event = request.GetRollBackEvent();
                @event.AddNotification([.. _domainNotification.Notifications]);

                await _mediatorHandler.PublishEvent(@event);

                response = request.GetCommandOutput();
            }

            return response;
        }
    }
}
