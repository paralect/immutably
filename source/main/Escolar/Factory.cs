using System;
using Escolar.Aggregates;
using Escolar.Messages;
using Escolar.States;
using Paralect.Machine.Processes;

namespace Escolar
{
    public interface IFactory
    {
        Object CreateObject(Type type);
        Object CreateAggregate(Type type);
        
        IStateEnvelope CreateStateEnvelope(IState state, IStateMetadata metadata);
        IStateMetadata CreateStateMetadata(Guid entityId, int version);
        IStateSpooler CreateStateSpooler(IStateEnvelope initialStateEnvelope);
        IStateHelper CreateStateHelper();
        IAggregateHelper CreateAggregateHelper();
    }

    public class Factory : IFactory
    {
        public Object CreateObject(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public Object CreateAggregate(Type type)
        {
            return CreateObject(type);
        }

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

        public IAggregateHelper CreateAggregateHelper()
        {
            return new AggregateHelper(this);
        }

        public IStateHelper CreateStateHelper()
        {
            return new StateHelper(this);
        }
    }
}