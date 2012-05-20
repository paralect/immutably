using System;

namespace Immutably
{
    public interface IFactory
    {
        Object CreateObject(Type type);
        Object CreateAggregate(Type type);
        Object CreateState(Type type);
        
/*        IStateEnvelope CreateStateEnvelope(IState state, IStateMetadata metadata);
        IStateMetadata CreateStateMetadata(Guid entityId, int version);
        IStateSpooler CreateStateSpooler(IStateEnvelope initialStateEnvelope);
        //IAggregateSession CreateAggregateSession(ITransitionStore transitionStore);
        IAggregateStore CreateAggregateStore(ITransitionStore transitionStore);
        ITransitionStore CreateTransitionStore();
  */      
    }
}