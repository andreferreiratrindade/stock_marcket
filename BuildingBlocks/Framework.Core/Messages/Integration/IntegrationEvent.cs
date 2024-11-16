﻿using Framework.Core.DomainObjects;
using Framework.Core.Messages;

namespace Framework.Core.Messages.Integration
{
    public abstract class IntegrationEvent : DomainEvent
    {

         protected IntegrationEvent(CorrelationId CorrelationId):base(CorrelationId){}
    }
}
