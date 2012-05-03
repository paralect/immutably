using System;
using Escolar.Aggregates;
using Escolar.Messages;
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
        IAggregateHelper CreateAggregateHelper();
        IAggregateSession CreateAggregateSession(ITransitionStore transitionStore);
        IAggregateStore CreateAggregateStore(ITransitionStore transitionStore);
        ITransitionStore CreateTransitionStore();
        
    }

    public class DefaultFactory : IFactory
    {
        #region Object creation
        
        public Object CreateObject(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public Object CreateAggregate(Type type)
        {
            return CreateObject(type);
        }

        public Object CreateState(Type type)
        {
            return CreateObject(type);
        }

        #endregion

        #region State related factories

        public IStateEnvelope CreateStateEnvelope(IState state, IStateMetadata metadata)
        {
            return new StateEnvelope(state, metadata);
        }

        public IStateMetadata CreateStateMetadata(Guid entityId, int version)
        {
            return new StateMetadata(entityId, version);
        }

        public IStateSpooler CreateStateSpooler(IStateEnvelope initialStateEnvelope)
        {
            return new StateSpooler(initialStateEnvelope);
        }

        public IStateHelper CreateStateHelper()
        {
            return new StateHelper(this);
        }

        #endregion

        #region Aggregate related factories

        public IAggregateHelper CreateAggregateHelper()
        {
            return new AggregateHelper(this);
        }

        public IAggregateSession CreateAggregateSession(ITransitionStore transitionStore)
        {
            return new AggregateSession(this, transitionStore);
        }

        public IAggregateStore CreateAggregateStore(ITransitionStore transitionStore)
        {
            return new AggregateStore(this, transitionStore);
        }

        #endregion

        public ITransitionStore CreateTransitionStore()
        {
            return new InMemoryTransitionStore();
        }


    }
}