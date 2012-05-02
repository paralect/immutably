using System;
using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public interface IAggregateHelper
    {
        Type GetStateType(Type aggregateType);
        IStateEnvelope CreateInitialStateEnvelope(Type aggregateType, Guid aggregateId);
    }

    public class AggregateHelper : IAggregateHelper
    {
        private readonly IFactory _factory;

        public AggregateHelper(IFactory factory)
        {
            _factory = factory;
        }

        public Type GetStateType(Type aggregateType)
        {
            var genericArgs = aggregateType.GetGenericArguments();
            var stateType = genericArgs[0];
            return stateType;
        }

        public IStateEnvelope CreateInitialStateEnvelope(Type aggregateType, Guid aggregateId)
        {
            var stateType = GetStateType(aggregateType);

            var state = (IState)_factory.CreateObject(stateType);
            var stateMetadata = _factory.CreateStateMetadata(aggregateId, 0);
            var stateEnvelope = _factory.CreateStateEnvelope(state, stateMetadata);

            return stateEnvelope;
        }
         
    }
}