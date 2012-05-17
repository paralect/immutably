using System;
using Escolar.Aggregates;
using Escolar.States;
using Escolar.Transitions;
using Paralect.Machine.Processes;

namespace Escolar
{
    public interface IFactory
    {
        Object CreateObject(Type type);
        Object CreateAggregate(Type type);
        Object CreateState(Type type);
        
        IStateEnvelope CreateStateEnvelope(IState state, IStateMetadata metadata);
        IStateMetadata CreateStateMetadata(Guid entityId, int version);
        IStateSpooler CreateStateSpooler(IStateEnvelope initialStateEnvelope);
        IStateHelper CreateStateHelper();
        //IAggregateSession CreateAggregateSession(ITransitionStore transitionStore);
        IAggregateStore CreateAggregateStore(ITransitionStore transitionStore);
        ITransitionStore CreateTransitionStore();
        
    }
}