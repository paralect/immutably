using System;
using Escolar.Messages;

namespace Escolar.Transitions
{
    public interface ITransitionBuilder
    {
        ITransitionBuilder AddEvent(IEvent evnt);
        ITransitionBuilder AddEvent(IEvent evnt, IEventMetadata metadata);
        ITransitionBuilder AddEvent(IEventEnvelope envelope);        
    }
}